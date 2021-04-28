// FlightPath.cs - Air 3550 Project
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
 * A monster class used to handle computations
 * and properties dealing with a list of
 * interconnected flights. Envision the flight
 * path as a way to get from airport A to airport
 * B with either one, two, or three flights.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Air_3550.Models;

namespace Database.Util
{
    public class FlightPath
    {
        // The list of flights that make up this flight path.
        public List<Flight> Flights;

        // Lazy properties for all of our computed properties
        // that we probably want to avoid recomputing every
        // single time we access them - that could get very
        // slow.
        private readonly Lazy<string> _formattedDepartureTime;
        private readonly Lazy<string> _formattedArrivalTime;
        private readonly Lazy<decimal> _price;
        private readonly Lazy<int> _priceInPoints;
        private readonly Lazy<List<TimeSpan>> _flightDepartureTimeline;
        private readonly Lazy<TimeSpan> _duration;

        // The departure time of the first flight, formatted nicely.
        public string FormattedDepartureTime => _formattedDepartureTime.Value;

        // The arrival time of the last flight, formatted nicely.
        public string FormattedArrivalTime => _formattedArrivalTime.Value;

        // The price of all the flights together.
        public decimal Price => _price.Value;

        // The price of all the flights together in points.
        public int PriceInPoints => _priceInPoints.Value;

        // The total time it would take if a customer flew the
        // entire flight path, starting from the first flight's
        // origin airport and going to the last flight's destination
        // airport.
        public TimeSpan Duration => _duration.Value;

        // A list of TimeSpans representing when in a timeline that
        // each flight departs. The first flight in the flight path
        // ALWAYS has a flight departure timeline of (0, 0, 0). The
        // indexing of this list corresponds to the indexing of the
        // flights list, so FlightDepartureTimeline[0] would correspond
        // to the flight departure timeline for the first flight in the
        // list.
        public List<TimeSpan> FlightDepartureTimeline => _flightDepartureTimeline.Value;

        // A property that simply returns the first flight's departure time.
        public TimeSpan FirstFlightDepartureTime => Flights.First().DepartureTime;

        // A property that simply returns the last flight's arrival time.
        public TimeSpan LastFlightArrivalTime => Flights.Last().GetArrivalTime();

        // Convenience methods to retrieve various
        // airport codes of first and last flights.
        public string FirstFlightDepartureAirportCode => Flights.First().OriginAirport.Code;
        public string FirstFlightArrivalAirportCode => Flights.First().DestinationAirport.Code;
        public string LastFlightDepartureAirportCode => Flights.Last().OriginAirport.Code;
        public string LastFlightArrivalAirportCode => Flights.Last().DestinationAirport.Code;

        public FlightPath(params Flight[] flights)
        {
            // We start by setting our flights list to
            // a list compromised of the flights from
            // the flights array that is passed in.
            Flights = new(flights);

            // We first setup the lazy fields for
            // the formatted departure and arrival
            // times.
            _formattedDepartureTime = new(() => FirstFlightDepartureTime.FormatAsTimeNicely());
            _formattedArrivalTime = new(() => LastFlightArrivalTime.FormatAsTimeNicely());

            // We now setup lazy fields for the pricing
            // of the flight path, by deferring to the
            // Pricing class. We also setup the price
            // in terms of points, which is simply the
            // price times 100 casted to an integer,
            // because a point corresponds to one cent.
            _price = new(() => Pricing.CalculatePriceOfFlights(Flights));
            _priceInPoints = new(() => (int)(Price * 100));

            // Now, let's take care of the doozy.
            // We need to setup the flight departure
            // timeline, which plots out the departure
            // times of each flight in a flight path.
            _flightDepartureTimeline = new(() =>
            {
                // We first construct a new list for
                // our flight departure timeline.
                var flightDepartureTimeline = new List<TimeSpan>();

                // We now setup a timespan which will help
                // us setup and traverse through the timeline.
                var timeSpan = new TimeSpan();

                // For the first flight, we just add its duration directly,
                // and its departure time is at 0 hours, 0 minutes, and 0 seconds.
                flightDepartureTimeline.Add(new TimeSpan(0, 0, 0));
                timeSpan += Flights[0].GetDuration();

                // We setup two TimeSpan constants, one for
                // fourty minutes and another for one day so
                // we aren't repeatedly constructing structures.
                TimeSpan fourtyMinutes = new(0, 40, 0);
                TimeSpan oneDay = new(1, 0, 0, 0);

                // Now we loop through each flight, skipping
                // the first one, and determine each's departure
                // timeline.
                for (int i = 1; i < Flights.Count; i++)
                {
                    // We get the previous and current
                    // flights of the loop.
                    var previousFlight = Flights[i - 1];
                    var flight = Flights[i];

                    // We first start by calculating the layover
                    // (the distance between the current and previous
                    // flights).
                    TimeSpan layover;

                    // We have two cases to handle here. If the current flight's departure time
                    // is less than the previous flight's arrival time (plus a fourty minute minimum
                    // layover period), and otherwise. In the first case, we have to go for the next
                    // day's flight, while in the second, we can just use the same day's flight.
                    if (flight.DepartureTime < previousFlight.GetArrivalTime().Add(fourtyMinutes))
                    {
                        // The flight departs before the previous flight arrives (plus the 40 minute minimum layover), so we
                        // need to proceed to the next day to determine the proper flight duration. Our layover is a day
                        // minus the difference of the previous flight's arrival time and the current flight's departure time.
                        layover = oneDay - (previousFlight.GetArrivalTime() - flight.DepartureTime);
                    }
                    else
                    {
                        // Because the flight departs after the previous flight + a 40 minute layover,
                        // we just calculate the layover as the current flight's departure time minus
                        // the previous one's arrival time.
                        layover = flight.DepartureTime - previousFlight.GetArrivalTime();
                    }

                    // For this flight, it's departure time occurs at our timespan + the layover.
                    flightDepartureTimeline.Add(timeSpan + layover);

                    // Finally, we add the flight duration to our timespan.
                    timeSpan += layover + flight.GetDuration();
                }

                return flightDepartureTimeline;
            });

            _duration = new(() =>
            {
                // If we take the time the last flight departs in the timeline and
                // add it's duration, we get the total duration of the flight path.
                return FlightDepartureTimeline.Last() + Flights.Last().GetDuration();
            });
        }

        // This computed property determines the
        // max layover of a flight path and returns
        // it.
        public TimeSpan MaxLayoverDuration
        {
            get
            {
                // We start with a max layover duration of 0. In
                // the case of a flight path with only one flight,
                // we will just immediately return zero.
                var maxLayoverDuration = new TimeSpan(0, 0, 0);

                // We now loop through all the flights skipping the first one.
                for (int i = 1; i < FlightDepartureTimeline.Count; i++)
                {
                    // We now grab the departure times of both flights with the timeline.
                    var previousFlightDepartureTime = FlightDepartureTimeline[i - 1];
                    var currentFlightDepartureTime = FlightDepartureTimeline[i];

                    // The layover duration is simply calculated by grabbing the previous flight's
                    // arrival time (which is simply calculated by adding the previous departure time
                    // to the flight duration) and subtracting it from the current flight's departure
                    // time.
                    var layoverDuration = currentFlightDepartureTime - (previousFlightDepartureTime + Flights[i - 1].GetDuration());

                    // If the max layover duration is less than the one we
                    // just calculated, we update the max layover duration
                    // to this new duration.
                    if (maxLayoverDuration < layoverDuration)
                    {
                        maxLayoverDuration = layoverDuration;
                    }
                }

                // Finally, we return the max layover duration.
                return maxLayoverDuration;
            }
        }

        // A computed property to return the number of stops,
        // which is simply the flight count minus one.
        public int NumberOfStops => Flights.Count - 1;

        // A computed property that formats the price of
        // the flight as money to make it easier to bind
        // to the price in the user interface.
        public string FormattedPrice => Price.FormatAsMoney();

        // A computed property that formats the duration
        // nicely for easy binding in the user interface.
        public string FormattedDuration => Duration.FormatAsDurationNicely();

        // A computed property that formats the number of
        // stops nicely so that it canb e bound to the UI
        // and display nicely.
        public string FormattedStops
        {
            get
            {
                return NumberOfStops switch
                {
                    0 => "Nonstop",
                    1 => NumberOfStops + " stop (" + Flights[0].DestinationAirport.Code + ")",
                    _ => NumberOfStops + " stops (" + Flights[0].DestinationAirport.Code + ", " + Flights[1].DestinationAirport.Code + ")"
                };
            }
        }
    }
}
