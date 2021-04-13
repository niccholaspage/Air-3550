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
            DateTime date2 = DateTime.Now;
            foreach (var ticket in tickets)
            {
                distance += ticket.ScheduledFlight.Flight.GetDistance();
                totalCost += 8;
                date2 = ticket.ScheduledFlight.GetArrivalTimestamp();
                if (ticket.ScheduledFlight.DepartureTimestamp < date1) 
                {                                                       
                    date1 = ticket.ScheduledFlight.DepartureTimestamp;
                }
                if(ticket.ScheduledFlight.GetArrivalTimestamp() > date2)
                {
                    date2 = ticket.ScheduledFlight.GetArrivalTimestamp();
                }
            }
            totalCost += (0.12 * distance);
            if (date1.Hour < 5 || date2.Hour < 5) totalCost -= totalCost * 0.20;
            else if (date1.Hour < 8 || date2.Hour > 19) totalCost -= totalCost * 0.10;
            return totalCost;
        }


        public double GetTotalCost()
        {
            return GetCost(true) + GetCost(false);
        }

        // this calculates the duration of one-way flight (either departure or return)
        public TimeSpan GetDuration(bool departingTickets)
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            TimeSpan totalDuration = new TimeSpan();
            TimeSpan finalFlightTime = new TimeSpan();
            var tickets = departingTickets ? GetDepartureTickets() : GetReturnTickets();
            if (tickets.Count == 0)
            {
                return totalDuration;
            }
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
                    finalFlightTime = ticket.ScheduledFlight.Flight.GetFlightDuration();
                }
            }
            totalDuration = date2.Subtract(date1);
            totalDuration += finalFlightTime;
            return totalDuration;
        }

        // this calculates the duration of round-trip flight
        public TimeSpan GetTotalDuration()
        {
            return GetDuration(true) + GetDuration(false);
        }
    }
}
