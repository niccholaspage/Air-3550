using Air_3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Util
{
    public class Pricing
    {

        private static readonly TimeSpan hourFive = new TimeSpan(0, 5, 0, 0);
        private static readonly TimeSpan hourEight = new TimeSpan(0, 8, 0, 0);
        private static readonly TimeSpan hourNineteen = new TimeSpan(0, 19, 0, 0);

        //TODO: Determine if this is a good resting place
        public static decimal GetDiscountPercentage(TimeSpan departureTime, TimeSpan arrivalTime)
        {
            TimeSpan newArrivalTime = new TimeSpan(0, arrivalTime.Hours, arrivalTime.Minutes, arrivalTime.Seconds);
            if ((departureTime < hourFive) || (newArrivalTime < hourFive) )
            {
                return 0.20m;
            }
            else if (departureTime < hourEight || newArrivalTime > hourNineteen)
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
