﻿using Air_3550.Repository;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.Util
{
    class FlightSearchAlgorithm
    {
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
            using (var db = new AirContext())
            {
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

            // TODO: Optimize this - this is doing way to many queries and has tons of room for optimization.
            using (var db = new AirContext())
            {
                var index = 0;
                while (index < flightPaths.Count)
                {
                    var flightPath = flightPaths[index];

                    var departureDateAndTime = departureDate + flightPath.FirstFlightDepartureTime;

                    // Check if the first flight of the flight path has already departed. If so,
                    // remove the flight path.
                    if (DateTime.Now >= departureDateAndTime)
                    {
                        flightPaths.RemoveAt(index);

                        continue;
                    }

                    var flightDepartureTimeline = flightPath.FlightDepartureTimeline;

                    for (int i = 0; i < flightPath.Flights.Count; i++)
                    {
                        var flight = flightPath.Flights[i];

                        var flightDepartureDate = (departureDateAndTime + flightDepartureTimeline[i]).Date;

                        // TODO: Why on earth does the DepartureDate comparision only work if
                        // we turn the query into a list then check for it?
                        var allScheduledFlightsForFlightAsList = await (from scheduledFlight in db.ScheduledFlights
                                                                        where scheduledFlight.Flight == flight
                                                                        select new { scheduledFlight.DepartureDate, TicketCount = scheduledFlight.Tickets.Count, PlaneCapacity = scheduledFlight.Flight.Plane.MaxSeats })
                                                                        .ToListAsync();

                        if (allScheduledFlightsForFlightAsList.Any(scheduledFlight => scheduledFlight.DepartureDate == flightDepartureDate && scheduledFlight.TicketCount >= scheduledFlight.PlaneCapacity))
                        {
                            flightPaths.RemoveAt(index);
                        }
                        else
                        {
                            index++;
                        }
                    }
                }
            }

            return flightPaths;
        }
    }
}
