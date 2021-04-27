﻿// Flight.cs - Air 3550 Project
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

using Database.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Air_3550.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required]
        public int Number { get; set; }

        public int OriginAirportId { get; set; }

        [Required]
        public Airport OriginAirport { get; set; }

        public int DestinationAirportId { get; set; }

        [Required]
        public Airport DestinationAirport { get; set; }

        [Required]
        public TimeSpan DepartureTime { get; set; }

        public int PlaneId { get; set; }

        [Required]
        public Plane Plane { get; set; }

        public bool IsCanceled { get; set; }

        [NotMapped]
        public string FormattedDepartureTime => DepartureTime.FormatAsTimeNicely();

        [NotMapped]
        public string FormattedArrivalTime => GetArrivalTime().FormatAsTimeNicely();

        /**
         * Calculate distance between the origin airport and destination
         * airport. Returns the distance in miles.
         * 
         */
        public double GetDistance()
        {
            static double toRadians(decimal angdeg)
            {
                return (double)(Math.PI * (double)angdeg) / 180;
            }

            const int R = 3963; // Radius of the earth in miles

            double latitudeDistance = toRadians(DestinationAirport.Latitude - OriginAirport.Latitude);
            double longitudeDistance = toRadians(DestinationAirport.Longitude - OriginAirport.Longitude);
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

        // this calculates the duration of each flight
        public TimeSpan GetDuration()
        {
            double duration = 30.0 + GetDistance() * (60.0 / 500.0);
            int hours = (int)duration / 60;
            int min = (int)duration % 60;
            return new(hours, min, 0);
        }

        public decimal GetCost()
        {
            decimal flightCost = 0;
            double duration = GetDistance();
            flightCost += Convert.ToDecimal(duration) * 0.12m;
            flightCost *= 1m - Pricing.GetDiscountPercentage(DepartureTime, GetArrivalTime());
            return Math.Truncate(100 * flightCost) / 100;
        }
    }
}
