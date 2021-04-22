using Database.Util;
using System;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class ScheduledFlight
    {
        public int ScheduledFlightId { get; set; }

        [Required]
        public Flight Flight { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        public DateTime GetDepartureTimestamp()
        {
            return DepartureDate.Add(Flight.DepartureTime);
        }

        public DateTime GetArrivalTimestamp()
        {
            return GetDepartureTimestamp().Add(Flight.GetDuration());
        }

        public decimal GetCost()
        {
            decimal flightCost = 0;
            double duration = Flight.GetDistance();
            flightCost += Convert.ToDecimal(duration) * 0.12m;
            flightCost *= 1m - Pricing.GetDiscountPercentage(GetDepartureTimestamp(), GetArrivalTimestamp());
            return Math.Truncate(100 * flightCost) / 100;
        }
    }
}
