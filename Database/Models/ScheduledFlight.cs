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
            if (GetDepartureTimestamp().Hour < 5 || GetArrivalTimestamp().Hour < 5)
            {
                flightCost -= flightCost * 0.20m;
            }
            else if (GetDepartureTimestamp().Hour < 8 || GetArrivalTimestamp().Hour > 19)
            {
                flightCost -= flightCost * 0.10m;
            }
            return flightCost;
        }
    }
}
