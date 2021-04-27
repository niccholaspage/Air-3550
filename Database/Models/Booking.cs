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

/**
 * The booking model is used to represent a
 * booking/trip that a customer makes. A booking
 * includes tickets (which are per scheduled flight)
 * for both the departure path and return path. While
 * tickets are stored in a single list, the model has
 * convenient accessors to get only departure or
 * return tickets.
 */

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

        public int CustomerDataId { get; set; } // The ID of the customer data this booking is attached to.

        // The customer data this booking is attached to, as each customer data has a list of bookings.
        [Required]
        public CustomerData CustomerData { get; set; }

        // The list of tickets attached to each booking. In our system, a ticket is created
        // per scheduled flight, so we group tickets up by booking to present the user a list
        // of their bookings/trips comprised of all their tickets.
        public List<Ticket> Tickets { get; } = new();

        // The index of the first return ticket. If this is null, then this booking is
        // one-way. Otherwise, this booking is guaranteed to be a round-trip, and this
        // variable will refer to the first index of the return ticket for use in future
        // computed properties.
        public int? FirstReturnTicketIndex { get; set; }

        // A computed property that returns whether all of the tickets in a booking can be canceled.
        // Every ticket in a booking can be canceled if the first ticket in the booking is not already
        // canceled and has not departed yet and will not be departing for at least an hour.
        [NotMapped]
        public bool CanCancelAllTickets => !Tickets.First().IsCanceled && DateTime.Now <= DepartureFlightPathWithDate.FirstDepartureFlightTimestamp.AddHours(-1);

        // A computed property that returns whether the return tickets in a booking can be canceled.
        // The return tickets can be canceled if this booking contains return tickets and first return ticket
        // in the booking is not already canceled and has not departed yet and will not be departing for at
        // least an hour.
        [NotMapped]
        public bool CanCancelReturnTickets => HasReturnTickets && !GetReturnTickets().First().IsCanceled && !CanCancelAllTickets && DateTime.Now <= ReturnFlightPathWithDate.FirstDepartureFlightTimestamp.AddHours(-1);

        // A computed property that simply returns whether some form of cancelation can be done
        // on this booking so that we can display a cancel button to the user.
        [NotMapped]
        public bool CanCancelSomeTickets => CanCancelAllTickets || CanCancelReturnTickets;

        // A computed property to check if the departure tickets of a booking have been canceled.
        [NotMapped]
        public bool AreDepartureTicketsCanceled => Tickets.First().IsCanceled;

        // A computed property to check if the return tickets of a booking have been canceled.
        [NotMapped]
        public bool AreReturnTicketsCanceled
        {
            get
            {
                var returnTickets = GetReturnTickets();

                return returnTickets.Count > 0 && returnTickets.First().IsCanceled;
            }
        }

        // A computed property that returns the departure flight path with date
        // of the booking based on its departing tickets. This is used to determine
        // the duration, departure and arrival times, price, and other information of
        // the flight path.
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


        // A computed property that returns the return flight path with date
        // of the booking based on its returning tickets. This is used to determine
        // the duration, departure and arrival times, price, and other information of
        // the flight path. If the booking does not have return tickets, it just returns
        // null.
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

        // A computed property that simply returns whether a booking has return tickets or not.
        [NotMapped]
        public bool HasReturnTickets => FirstReturnTicketIndex != null;

        // A computed property that returns the type of the booking.
        [NotMapped]
        public string Type => HasReturnTickets ? "Round Trip" : "One Way";

        // A method that returns the departure tickets of a booking.
        // Since a booking contains a list of all tickets associated with
        // it, to get just the departing tickets, we must return a subrange
        // of the tickets list from the first item to right before the first
        // return index. If this booking does not have a first return index,
        // we just return all the tickets.
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

        // A method that returns the return tickets of a booking.
        // Since a booking contains a list of all tickets associated with
        // it, to get just the return tickets, we must return a subrange
        // of the tickets list from the first return index to the end.
        // If this booking does not have a first return index,
        // we just return an empty list.
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
