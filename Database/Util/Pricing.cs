using Air_3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Util
{
    public class Pricing
    {
        //TODO: Determine if this is a good resting place
        public static decimal GetDiscountPercentage(TimeSpan departureTime, TimeSpan arrivalTime)
        {
            if ((departureTime.Hours > 0 && departureTime.Hours < 5) || (arrivalTime.Hours > 0 && arrivalTime.Hours < 5))
            {
                return 0.20m;
            }
            else if (departureTime.Hours < 8 || arrivalTime.Hours > 19)
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
