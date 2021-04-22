using System;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        [Required]
        public ScheduledFlight ScheduledFlight { get; set; }

        public bool IsCanceled { get; set; }

        [Required]
        public Booking Booking { get; set; }


        public decimal GetCost()
        {
            decimal flightCost = 0;
            double duration = ScheduledFlight.Flight.GetDistance();
            flightCost += Convert.ToDecimal(duration) * 0.12m;
            if (ScheduledFlight.GetDepartureTimestamp().Hour < 5 || ScheduledFlight.GetArrivalTimestamp().Hour < 5)
            {
                flightCost -= flightCost * 0.20m;
            }
            else if (ScheduledFlight.GetDepartureTimestamp().Hour < 8 || ScheduledFlight.GetArrivalTimestamp().Hour > 19)
            {
                flightCost -= flightCost * 0.10m;
            }
            return flightCost;
        }
    }
}
