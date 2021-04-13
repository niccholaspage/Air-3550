using System;
using System.Collections.Generic;

namespace Air_3550.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public List<Ticket> Tickets { get; set; }

        private int GetFirstReturnTicketIndex()
        {
            // TODO: See if reference equality is enough,
            // or if we will need to be checking IDs.
            Ticket previousTicket = null;

            for (int i = 0; i < Tickets.Count; i++)
            {
                Ticket ticket = Tickets[i];

                if (previousTicket != null)
                {
                    if (previousTicket.ScheduledFlight.Flight.DestinationAirport == ticket.ScheduledFlight.Flight.OriginAirport)
                    {
                        return i;
                    }
                }

                previousTicket = ticket;
            }

            return -1;
        }

        public List<Ticket> GetDepartureTickets()
        {
            int index = GetFirstReturnTicketIndex();

            if (index == -1)
            {
                return Tickets;
            }
            else
            {
                return Tickets.GetRange(0, index);
            }
        }

        public List<Ticket> GetReturnTickets()
        {
            int index = GetFirstReturnTicketIndex();

            if (index == -1)
            {
                return new List<Ticket>();
            }
            else
            {
                return Tickets.GetRange(index, Tickets.Count);
            }
        }

        private double GetCost(bool departingTickets)
        {
            var tickets = departingTickets ? GetDepartureTickets() : GetReturnTickets();
            if (tickets.Count == 0)
            {
                return 0.0;
            }
            double totalCost = 50;
            double distance = 0;
            DateTime date1 = DateTime.Now; // TODO: This needs to be updated, why are we using current time?
            foreach (var ticket in tickets)
            {
                distance += ticket.ScheduledFlight.Flight.GetDistance();
                totalCost += 8;
                if (ticket.ScheduledFlight.DepartureTimestamp < date1) // still need to implement the discount based
                {                                                       // on arrival time
                    date1 = ticket.ScheduledFlight.DepartureTimestamp;
                }
            }
            totalCost += (0.12 * distance);
            if (date1.Hour < 8) totalCost -= totalCost * 0.10;
            else if (date1.Hour < 5) totalCost -= totalCost * 0.20;
            return totalCost;
        }


        public double GetTotalCost()
        {
            return GetCost(true) + GetCost(false);
        }


        public TimeSpan GetTotalDuration()
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            TimeSpan totalDuration = new TimeSpan();
            foreach (var ticket in Tickets)
            {
                date2 = ticket.ScheduledFlight.DepartureTimestamp;
                if (ticket.ScheduledFlight.DepartureTimestamp < date1)
                {
                    date1 = ticket.ScheduledFlight.DepartureTimestamp;
                }
                if (ticket.ScheduledFlight.DepartureTimestamp > date2)
                {
                    date2 = ticket.ScheduledFlight.DepartureTimestamp;
                }
            }
            totalDuration = date2.Subtract(date1);
            return totalDuration;
        }
    }
}
