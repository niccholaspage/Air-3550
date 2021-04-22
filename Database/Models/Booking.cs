﻿using System;
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

        private decimal GetCost(bool departingTickets)
        {
            var tickets = departingTickets ? GetDepartureTickets() : GetReturnTickets();
            if (tickets.Count == 0)
            {
                return 0.0m;
            }
            int basePrice = 50;
            decimal totalCost = 0;
            totalCost += basePrice;
            DateTime date1 = DateTime.Now; // TODO: This needs to be updated, why are we using current time?
            DateTime date2;
            date2 = tickets[0].ScheduledFlight.GetArrivalTimestamp();
            foreach (var ticket in tickets)
            {
                totalCost += ticket.GetCost();
                if (ticket.ScheduledFlight.GetDepartureTimestamp() < date1)
                {
                    date1 = ticket.ScheduledFlight.GetDepartureTimestamp();
                }
                if (ticket.ScheduledFlight.GetArrivalTimestamp() > date2)
                {
                    date2 = ticket.ScheduledFlight.GetArrivalTimestamp();
                }
            }
            totalCost += (8 * (tickets.Count - 1));
            if (date1.Hour < 5 || date2.Hour < 5) totalCost -= basePrice * 0.20m;
            else if (date1.Hour < 8 || date2.Hour > 19) totalCost -= basePrice * 0.10m;
            return totalCost;
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
