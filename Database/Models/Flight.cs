// Flight.cs - Air 3550 Project
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
 * The flight model is used to represent a flight
 * in the database. In our system, a flight is a
 * recurring flight that happens EVERY single day
 * unless it is canceled. Flights contain a flight
 * number, their origin and destination airport,
 * the plane model they are using, and a departure
 * time. Flights do NOT contain a departure date,
 * as they represent a recurring event.
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Util;

namespace Air_3550.Models
{
    public class Flight
    {
        public int FlightId { get; set; } // The primary ID of the flight, used to relate the flight to other data.

        // The number of the flight. While in practice this corresponds to the ID,
        // it allows for future support for number rollover if necessary.
        [Required]
        public int Number { get; set; }

        // The ID of the origin airport that is attached to this flight.
        public int OriginAirportId { get; set; }

        // The origin airport for this flight, which is the airport this flight departs from.
        [Required]
        public Airport OriginAirport { get; set; }

        // The ID of the destination airport that is attached to this flight.
        public int DestinationAirportId { get; set; }

        // The destination airport for this flight, which is the airport this flight arrives at.
        [Required]
        public Airport DestinationAirport { get; set; }

        // The departure time of this flight, represented
        // as a TimeSpan with the hours and minutes it departs.
        [Required]
        public TimeSpan DepartureTime { get; set; }

        // The ID of the plane that this flight uses.
        public int PlaneId { get; set; }

        // The plane model used for this flight. This determines how many seats
        // any scheduled flight based off of this flight will have.
        [Required]
        public Plane Plane { get; set; }

        // Whether this flight is canceled or not. If it is canceled, it will not
        // be used when determining flight paths for a user, and will be hidden
        // from the system in numerous areas.
        public bool IsCanceled { get; set; }

        // A computed property formatting the departure time of the flight nicely
        // for UI bindings.
        [NotMapped]
        public string FormattedDepartureTime => DepartureTime.FormatAsTimeNicely();

        // A computed property formatting the arrival time of the flight nicely
        // for UI bindings.
        [NotMapped]
        public string FormattedArrivalTime => GetArrivalTime().FormatAsTimeNicely();

        // A method that calculates the distance between the origin and destination airport
        // based on their latitudes and longitudes using the Haversine method. This was
        // implemented based on an implementation in Java as seen here:
        // https://stackoverflow.com/questions/3694380/calculating-distance-between-two-points-using-latitude-longitude
        // Our version does not take into account height differences between the airports,
        // as we do not need that level of precision.
        public double GetDistance()
        {
            // A helper method for converting degrees into radians.
            static double toRadians(decimal angdeg)
            {
                return (double)(Math.PI * (double)angdeg) / 180;
            }

            const int R = 3963; // Radius of the Earth in miles

            double latitudeDistance = toRadians(DestinationAirport.Latitude - OriginAirport.Latitude);      // The distance between the two latitudes in radians.
            double longitudeDistance = toRadians(DestinationAirport.Longitude - OriginAirport.Longitude);   // The distance between the two longitudes in radians.

            double a = Math.Sin(latitudeDistance / 2) * Math.Sin(latitudeDistance / 2)
                    + Math.Cos(toRadians(OriginAirport.Latitude)) * Math.Cos(toRadians(DestinationAirport.Latitude))
                    * Math.Sin(longitudeDistance / 2) * Math.Sin(longitudeDistance / 2);

            double c = 2 * Math.Asin(Math.Sqrt(a)) * R;

            return c;
        }

        public TimeSpan GetArrivalTime()
        {
            return DepartureTime.Add(GetDuration());
        }

        // A method that calculates the duration of a flight. Since
        // each flight flies at the same speed, we can use constants
        // and easily find the flight duration.
        public TimeSpan GetDuration()
        {
            // We first start with 30 minutes (15 to get from the
            // gate to the en of the runaway, and another 15 to land
            // and taxi to the gate at the destination) then add
            // the distance in miles and multiply it by 60 minutes
            // divided by 500 miles per hour, which returns the
            // duration in minutes.
            double duration = 30.0 + GetDistance() * (60.0 / 500.0);

            // Because we are returning the duration as a TimeSpan,
            // we need to calclate the hours and minutes seperately,
            // so we divide our duration by 60 to get the hours,
            // and take the remainder to get the minutes.
            int hours = (int)duration / 60;
            int minutes = (int)duration % 60;

            // Finally, we construct a TimeSpan with our hours and minutes.
            return new(hours, minutes, 0);
        }

        // A method that returns the cost of a flight, but only
        // based on the variable cost of the flight due to its length.
        // This DOES NOT include the fixed $50 cost or TSA segment fees,
        // as this will be handled elsewhere in the FlightPath.
        public decimal GetCost()
        {
            decimal flightCost = 0; // We start with a flight cost of zero.
            double distance = GetDistance(); // We grab the distance of the flight.

            // We convert the distance to a decimal then multiply by 12 cents to
            // handle the specification of twelve cents per mile.
            flightCost += Convert.ToDecimal(distance) * 0.12m;

            // We then apply the discount based on the departure and
            // arrival time of the flight by deferring to our Pricing
            // class.
            flightCost *= 1m - Pricing.GetDiscountPercentage(DepartureTime, GetArrivalTime());

            // Finally, we only keep the last two decimals of the cost.
            // Hurray for savings to the customer!
            return Math.Truncate(100 * flightCost) / 100;
        }
    }
}
