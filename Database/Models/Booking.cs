using Database.Migrations;
using Database.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Air_3550.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int CustomerDataId { get; set; }

        [Required]
        public CustomerData CustomerData { get; set; }

        public List<Ticket> Tickets { get; } = new();

        public int? FirstReturnTicketIndex { get; set; }

        [NotMapped]
        public bool CanCancel => (!Tickets.First().IsCanceled)&&(DateTime.Now <= DepartureFlightPathWithDate.FirstDepartureFlightTimestamp.AddHours(-1));

        [NotMapped]
        public bool CanCancelReturn =>(HasReturnTickets && !GetReturnTickets().First().IsCanceled) && !CanCancel && DateTime.Now <= ReturnFlightPathWithDate.FirstDepartureFlightTimestamp.AddHours(-1);

        [NotMapped]
        public bool AreDepartureTicketsCanceled => Tickets.First().IsCanceled;

        public bool AreReturnTicketsCanceled
        {
            get
            {
                var returnTickets = GetReturnTickets();

                return returnTickets.Count > 0 && returnTickets.First().IsCanceled;
            }
        }

        [NotMapped]
        public FlightPathWithDate DepartureFlightPathWithDate
        {
            get
            {
                List<Ticket> DepartingTickets = GetDepartureTickets();

                FlightPath path = new(DepartingTickets.Select(Ticket => Ticket.ScheduledFlight.Flight).ToArray());

                return new FlightPathWithDate(path, DepartingTickets.First().ScheduledFlight.DepartureDate);
            }
        }

        [NotMapped]
        public FlightPathWithDate ReturnFlightPathWithDate
        {
            get
            {
                List<Ticket> ReturnTickets = GetReturnTickets();

                if (ReturnTickets.Count == 0)
                {
                    return null;
                }

                FlightPath path = new(ReturnTickets.Select(Ticket => Ticket.ScheduledFlight.Flight).ToArray());

                return new FlightPathWithDate(path, ReturnTickets.First().ScheduledFlight.DepartureDate);
            }
        }

        [NotMapped]
        public bool HasReturnTickets => FirstReturnTicketIndex != null;

        public List<Ticket> GetDepartureTickets()
        {
            if (FirstReturnTicketIndex is int index)
            {
                return Tickets.GetRange(0, index);
            }
            else
            {
                return Tickets;
            }
        }

        public List<Ticket> GetReturnTickets()
        {
            if (FirstReturnTicketIndex is int index)
            {
                return Tickets.TakeLast(Tickets.Count - index).ToList();
            }
            else
            {
                return new List<Ticket>();
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
    }
}
