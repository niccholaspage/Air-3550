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

/**
 * A static class used to calculate Pricing
 * for a list of flights as well as discounts
 * based on departure and arrival times.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Air_3550.Models;

namespace Database.Util
{
    public static class Pricing
    {
        // We setup a couple of TimeSpan constants to avoid having to
        // construct new ones every single time we calculate a discount
        // percentage.
        private static readonly TimeSpan HOUR_FIVE = new(0, 5, 0, 0);
        private static readonly TimeSpan HOUR_EIGHT = new(0, 8, 0, 0);
        private static readonly TimeSpan HOUR_NINETEEN = new(0, 19, 0, 0);

        // A method that calculates the discount percentage based on a
        // departure and arrival time. This is used for both calculating an
        // individual flight's cost and the discount on the fixed $50 fee.
        public static decimal GetDiscountPercentage(TimeSpan departureTime, TimeSpan arrivalTime)
        {
            // We first construct a new arrival time with days set to zero.
            // This basically ignores the days the arrival time might have,
            // to make our comparisions below work.
            TimeSpan newArrivalTime = new TimeSpan(0, arrivalTime.Hours, arrivalTime.Minutes, arrivalTime.Seconds);

            // If a flight departs or arrives before 5 AM and after midnight (implied by non-negative hours),
            if (departureTime < HOUR_FIVE || newArrivalTime < HOUR_FIVE)
            {
                return 0.20m; // we get a 20% discount.
            }
            // If a flight departs before 8 AM or arrives after 7 PM,
            else if (departureTime < HOUR_EIGHT || newArrivalTime > HOUR_NINETEEN)
            {
                return 0.10m; // we get a 10% discount.
            }
            else // Otherwise,
            {
                return 0.00m; // no discount!
            }
        }

        // This method calculates the total price
        // of a list of flights, as if they were
        // part of a flight path.
        public static decimal CalculatePriceOfFlights(List<Flight> flights)
        {
            // If we have no flights, we can't charge for anything,
            // so we just return a price of $0.00.
            if (flights.Count == 0)
            {
                return 0.0m;
            }

            // We start with the base price of 50, and apply a discount based on the
            // first flight's departure time and last flight's arrival time.
            decimal totalCost = 50 * (1m - GetDiscountPercentage(flights.First().DepartureTime, flights.Last().GetArrivalTime()));

            // Now we loop through each flight and add
            // the cost to the total.
            foreach (var flight in flights)
            {
                totalCost += flight.GetCost();
            }

            // Finally, we calculate and add the TSA segment fee,
            // which is a mandatory $8 fee per connecting flight.
            totalCost += 8 * (flights.Count - 1);

            return totalCost;
        }
    }
}
