using Air_3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Util
{
    public class Pricing
    {
        //TODO: Determine if this is a good resting place
        public static decimal GetDiscountPercentage(DateTime departureTimestamp, DateTime arrivalTimestamp)
        {
            if ((departureTimestamp.Hour > 0 && departureTimestamp.Hour < 5) || (arrivalTimestamp.Hour > 0 && arrivalTimestamp.Hour < 5))
            {
                return 0.20m;
            }
            else if (departureTimestamp.Hour < 8 || arrivalTimestamp.Hour > 19)
            {
                return 0.10m;
            }
            else
            {
                return 0.00m;
            }
        }

        public static decimal CalculatePriceOfScheduledFlights(List<ScheduledFlight> scheduledFlights)
        {
            if (scheduledFlights.Count == 0)
            {
                return 0.0m;
            }
            decimal basePrice = 50 * (1m - GetDiscountPercentage(scheduledFlights.First().GetDepartureTimestamp(), scheduledFlights.Last().GetArrivalTimestamp()));
            decimal totalCost = 0;
            totalCost += basePrice;
            foreach (var scheduledFlight in scheduledFlights)
            {
                totalCost += scheduledFlight.GetCost();
            }
            totalCost += (8 * (scheduledFlights.Count - 1));
            return totalCost;
        }
    }
}
