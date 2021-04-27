// Booking.cs - Air 3550 Project
//
// This program emulates a flight reservation system for a new airline,
// allowing customers to book and manage trips,as well as allowing employees
// to update the available flights and view stats on them.
//
// Authors:		Nicholas Nassar, Jacob Hammitte, Nikesh Dhital
// Class:		EECS 3550-001 Software Engineering, Spring 2021
// Instructor:	Dr. Thomas
// Date:		April 28, 2021
// Copyright:	Copyright 2021 by Nicholas Nassar, Jacob Hammitte, and Nikesh Dhital. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Database.Util;

namespace Air_3550.Models
{
    public class Booking
    {
        public int BookingId { get; set; } // The primary ID of the booking, used to relate the booking to other data.

        public int CustomerDataId { get; set; }

        [Required]
        public CustomerData CustomerData { get; set; }

        public List<Ticket> Tickets { get; } = new();

        public int? FirstReturnTicketIndex { get; set; }

        [NotMapped]
        public bool CanCancelAllTickets => (!Tickets.First().IsCanceled) && (DateTime.Now <= DepartureFlightPathWithDate.FirstDepartureFlightTimestamp.AddHours(-1));

        [NotMapped]
        public bool CanCancelReturnTickets => HasReturnTickets && !GetReturnTickets().First().IsCanceled && !CanCancelAllTickets && DateTime.Now <= ReturnFlightPathWithDate.FirstDepartureFlightTimestamp.AddHours(-1);

        [NotMapped]
        public bool CanCancelSomeTickets => CanCancelAllTickets || CanCancelReturnTickets;

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

        [NotMapped]
        public string Type => HasReturnTickets ? "Round Trip" : "One Way";

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
    }
}
