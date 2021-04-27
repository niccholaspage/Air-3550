// Pricing.cs - Air 3550 Project
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

using System;
using System.Collections.Generic;
using System.Linq;
using Air_3550.Models;

namespace Database.Util
{
    public class Pricing
    {

        private static readonly TimeSpan HOUR_FIVE = new(0, 5, 0, 0);
        private static readonly TimeSpan HOUR_EIGHT = new(0, 8, 0, 0);
        private static readonly TimeSpan HOUR_NINETEEN = new(0, 19, 0, 0);

        //TODO: Determine if this is a good resting place
        public static decimal GetDiscountPercentage(TimeSpan departureTime, TimeSpan arrivalTime)
        {
            TimeSpan newArrivalTime = new TimeSpan(0, arrivalTime.Hours, arrivalTime.Minutes, arrivalTime.Seconds);
            if (departureTime < HOUR_FIVE || newArrivalTime < HOUR_FIVE)
            {
                return 0.20m;
            }
            else if (departureTime < HOUR_EIGHT || newArrivalTime > HOUR_NINETEEN)
            {
                return 0.10m;
            }
            else
            {
                return 0.00m;
            }
        }

        public static decimal CalculatePriceOfFlights(List<Flight> flights)
        {
            if (flights.Count == 0)
            {
                return 0.0m;
            }
            decimal basePrice = 50 * (1m - GetDiscountPercentage(flights.First().DepartureTime, flights.Last().GetArrivalTime()));
            decimal totalCost = 0;
            totalCost += basePrice;
            foreach (var scheduledFlight in flights)
            {
                totalCost += scheduledFlight.GetCost();
            }
            totalCost += (8 * (flights.Count - 1));
            return totalCost;
        }
    }
}
