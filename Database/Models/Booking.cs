using Database.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Air_3550.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        public List<Ticket> Tickets { get; } = new();

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

        private decimal GetCost(bool departingTickets)
        {
            var tickets = departingTickets ? GetDepartureTickets() : GetReturnTickets();
            return Pricing.CalculatePriceOfFlights(tickets.Select(ticket => ticket.ScheduledFlight.Flight).ToList());
        }


        public decimal GetTotalCost()
        {
            return GetCost(true) + GetCost(false);
        }

        // this calculates the duration of one-way flight (either departure or return)
        public TimeSpan GetDuration(bool departingTickets)
        {
            DateTime date1 = DateTime.Now;
            DateTime date2;
            TimeSpan totalDuration = new();
            TimeSpan finalFlightTime = new();
            var tickets = departingTickets ? GetDepartureTickets() : GetReturnTickets();
            if (tickets.Count == 0)
            {
                return totalDuration;
            }
            date2 = tickets[0].ScheduledFlight.GetArrivalTimestamp();
            foreach (var ticket in Tickets)
            {
                if (ticket.ScheduledFlight.GetDepartureTimestamp() < date1)
                {
                    date1 = ticket.ScheduledFlight.GetDepartureTimestamp();
                }
                if (ticket.ScheduledFlight.GetDepartureTimestamp() > date2)
                {
                    date2 = ticket.ScheduledFlight.GetDepartureTimestamp();
                    finalFlightTime = ticket.ScheduledFlight.Flight.GetDuration();
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
