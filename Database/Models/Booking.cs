using System;
using System.Collections.Generic;

namespace Air_3550.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public List<Ticket> Tickets { get; set; }

        public double GetCost()
        {
            double totalCost = 50;
            double distance = 0;
            DateTime date1 = DateTime.Now;
            foreach(var ticket in Tickets)
            {
                distance += ticket.ScheduledFlight.Flight.GetDistance();
                totalCost += 8;
                if(ticket.ScheduledFlight.DepartureTimestamp < date1) // still need to implement the discount based
                {                                                       // on arrival time
                    date1 = ticket.ScheduledFlight.DepartureTimestamp;
                }
            }
            totalCost += (0.12 * distance);
            if (date1.Hour < 8) totalCost -= totalCost * 0.10;
            else if (date1.Hour < 5) totalCost -= totalCost * 0.20;
            return totalCost;
        }
    }
}
