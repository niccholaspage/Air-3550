using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
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


        public double GetCost()
        {
            double flightCost = 0;
            double duration = ScheduledFlight.Flight.GetDistance();
            flightCost += (duration * 0.12);
            if(ScheduledFlight.DepartureTimestamp.Hour < 5 || ScheduledFlight.GetArrivalTimestamp().Hour < 5)
            {
                flightCost -= (flightCost * 0.20);
            }
            else if(ScheduledFlight.DepartureTimestamp.Hour < 8 || ScheduledFlight.GetArrivalTimestamp().Hour > 19)
            {
                flightCost -= (flightCost * 0.10);
            }
            return flightCost;
        }
    }
}
