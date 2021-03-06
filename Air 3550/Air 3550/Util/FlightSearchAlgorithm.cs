// FlightSearchAlgorithm.cs - Air 3550 Project
//
// This program emulates a flight reservation system for a new airline,
// allowing customers to book and manage trips,as well as allowing employees
// to update the available flights and view stats on them.
//
// Authors:		Nicholas Nassar, Jacob Hammitte, Nikesh Dhital
// Class:		EECS 3550-001 Software Engineering, Spring 2021
// Instructor:	Dr. Thomas
// Date:		April 28, 2021
// Copyright:	Copyright 2021 by Nicholas Nassar, Jacob Hammitte, and Nikesh Dhital. All rights reserved.

/**
 * This static class implements the flight search algorithm which
 * determines the possible flight paths between two airports. It also
 * has the ability find all valid and optimized flight paths, which will
 * remove flight paths that would result in layover of more than eight
 * hours as well as removing flight paths that would result in an
 * overbooking if they were booked.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Air_3550.Repository;
using Database.Util;
using Microsoft.EntityFrameworkCore;

namespace Air_3550.Util
{
    static class FlightSearchAlgorithm
    {
        // This method finds all flight paths with zero, one, or two connections,
        // completely ignoring any date or timing of the scheduled flights - it
        // purely looks at the recurring flight and not scheduled flight.
        public static async Task<List<FlightPath>> FindFlightPaths(int DepartureAirportId, int ArrivalAirportId)
        {
            // This method finds all flight paths with zero, one, or two connections.
            // This method will always return flight paths with the least possible amount
            // of connections, because it terminates after at least a single flight path has
            // zero, one, or two connections.
            //
            // This method does not guarantee any order, and will not return the best path
            // a customer could take, but instead all possible paths within the least connections
            // possible, so it is up to the caller to sort the routes however they want,
            // potentially based on price and/or duration.
            using var db = new AirContext();

            // This query grabs all direct flights that have not been canceled going from
            // the departure airport to the arrival airport.
            var directFlights = await db.Flights
                .Include(flight => flight.OriginAirport)
                .Include(flight => flight.DestinationAirport)
                .Where(flight => !flight.IsCanceled && flight.OriginAirportId == DepartureAirportId && flight.DestinationAirportId == ArrivalAirportId).ToListAsync();

            // If at least one direct flight exists, just return flight paths for
            // the direct flights immediately.
            if (directFlights.Count != 0)
            {
                return directFlights.Select(flight => new FlightPath(flight)).ToList();
            }

            // Setup a query to return all non-canceled flights in the DB, including their origin and destination airport.
            var flights = db.Flights.Include(flight => flight.OriginAirport).Include(flight => flight.DestinationAirport).Where(flight => !flight.IsCanceled);

            // Let's now take a look for flight paths with one connecting flight.
            var query = from flight in flights // For each flight in the flight query,
                        where flight.OriginAirportId == DepartureAirportId // where the origin airport matches the departure airport,
                                                                           // and joining each connection in the flights query where the connection's detination equals the connection's origin,
                        join connection in flights on flight.DestinationAirportId equals connection.OriginAirportId
                        where connection.DestinationAirportId == ArrivalAirportId // and the connection's destination equals the arrival airport,
                        select new FlightPath(flight, connection); // construct a flight path made up of the original flight and the connection.

            var possiblePaths = await query.ToListAsync(); // Turn the results into a list,

            if (possiblePaths.Count != 0)
            {
                return possiblePaths; // and return them if we found any results.
            }

            // Since we couldn't find any direct flights or two legged flights,
            // now we look for three legged flights.
            var doubleConnectedQuery = from flight in flights // FOr each flight in the flight query,
                                       where flight.OriginAirportId == DepartureAirportId // where the origin airport matches the departure airport,
                                                                                          // and joining each connection where the origin airport of the connection matches the flight's destination airport,
                                       join firstConnection in flights on flight.DestinationAirportId equals firstConnection.OriginAirportId
                                       // and the first connection's destination airport equals the second connection's origin airport,
                                       join secondConnection in flights on firstConnection.DestinationAirportId equals secondConnection.OriginAirportId
                                       // and finally making sure the second connection's destination equals where we want to arrive,
                                       where secondConnection.DestinationAirportId == ArrivalAirportId
                                       select new FlightPath(flight, firstConnection, secondConnection); // construct a flight path made up of the original flight and two connections.

            return await doubleConnectedQuery.ToListAsync(); //  and return them as a list.
        }

        public static async Task<List<FlightPath>> GetValidAndOptimizedFlightPaths(List<FlightPath> flightPaths, DateTime departureDate)
        {
            // This method takes a list of flight paths, usually found from
            // the above FindFlightPaths method, and returns a sorted list of
            // valid flight paths for a certain date (each flight is checked
            // to confirm that it does not have a fully booked scheduled flight)
            // as well as optimized flight paths (any flight with a layover higher
            // than 8 hours is removed).

            // We first start by removing any flight path who's max layover is over
            // 8 hours.
            var eightHours = new TimeSpan(8, 0, 0);
            flightPaths.RemoveAll(path => path.MaxLayoverDuration > eightHours);

            // We first start by sorting the flight paths by the cheapest price,
            // or the shortest duration if the prices match.
            flightPaths.Sort((x, y) => x.Price == y.Price ? x.Duration.CompareTo(y.Duration) : x.Price.CompareTo(y.Price));

            // We setup a list of flight paths to flag for deletion.
            List<FlightPath> flaggedFlightPaths = new();

            // This could have been optimized. Clearly, we should not be doing
            // a query for every single flight of a flight path. If we wanted
            // better performance, we could have to worked to minimize the
            // number of queries to the database.
            using (var db = new AirContext())
            {
                foreach (var flightPath in flightPaths) // We loop through each flight path in our potential list of flight paths,
                {
                    // Get the departure date and time of the first flight in the flight path,
                    var departureDateAndTime = departureDate + flightPath.FirstFlightDepartureTime;

                    // Check if the first flight of the flight path has
                    // already departed. If so, remove the flight path.
                    if (DateTime.Now >= departureDateAndTime)
                    {
                        flaggedFlightPaths.Add(flightPath);

                        continue;
                    }

                    // Retrieve the flight departure timeline.
                    var flightDepartureTimeline = flightPath.FlightDepartureTimeline;

                    // Loop through each flight in the flight path
                    // so that we can check if it's corresponding
                    // scheduled flight for the departure date is
                    // not full.
                    for (int i = 0; i < flightPath.Flights.Count; i++)
                    {
                        var flight = flightPath.Flights[i]; // Grab the flight

                        // We grab the flight's departure date, which is simply the flight path's initial departure
                        // date and time plus the flight departure timeline for our filght. We then take the date of
                        // it, because we do not need the time component.
                        var flightDepartureDate = (departureDateAndTime + flightDepartureTimeline[i]).Date;

                        // Bug in EF core potentially: Why on earth does the DepartureDate comparision only
                        // work if we turn the query into a list then check for it? To avoid this odd issue,
                        // we simply queried for all scheduled flights corresponding to the flight and got
                        // them as a list, then checked the departure date and ticket count.
                        var allScheduledFlightsForFlightAsList = await (from scheduledFlight in db.ScheduledFlights
                                                                        where scheduledFlight.Flight == flight
                                                                        select new { scheduledFlight.DepartureDate, TicketCount = scheduledFlight.Tickets.Where(ticket => !ticket.IsCanceled).Count(), PlaneCapacity = scheduledFlight.Flight.Plane.MaxSeats })
                                                                        .ToListAsync();

                        // If any scheduled flight has the correct departure date
                        // and the ticket count is equal to or higher than the
                        // plane capacity, then we flag the flight path for removal.
                        if (allScheduledFlightsForFlightAsList.Any(scheduledFlight => scheduledFlight.DepartureDate == flightDepartureDate && scheduledFlight.TicketCount >= scheduledFlight.PlaneCapacity))
                        {
                            flaggedFlightPaths.Add(flightPath);

                            break;
                        }
                    }
                }
            }

            // Finally, we loop through and remove
            // all flagged flight paths from the list.
            foreach (var flightPath in flaggedFlightPaths)
            {
                flightPaths.Remove(flightPath);
            }

            // If we have more than eight possible flight
            // paths, we cut the list down to only keep
            // the eight best ones, to cut down on the
            // amount of choice some searches may result
            // in.
            if (flightPaths.Count > 8)
            {
                flightPaths = flightPaths.Take(8).ToList();
            }

            return flightPaths;
        }
    }
}
