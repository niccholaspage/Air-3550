// Ticket.cs - Air 3550 Project
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
 * The ticket model is used to represent a
 * ticket that a booking contains. Tickets are
 * constructed whenever a customer purchases a
 * booking. One is created for each scheduled flight
 * the customer will be taking.
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models;

namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; } // The primary ID of the ticket, used to relate the ticket to other data.

        [Required]
        public ScheduledFlight ScheduledFlight { get; set; } // The scheduled flight this ticket is for.

        public bool IsCanceled { get; set; } // Whether this ticket is canceled or not.

        public bool PointsEarned { get; set; } // Whether points have been earned for this ticket already.

        public PaymentMethod PaymentMethod { get; set; } // The payment method used to purchase this ticket.

        [Required]
        public Booking Booking { get; set; } // The booking contains this ticket.

        // A computed property that checks whether the boarding pass
        // for a ticket should be available. A boarding pass for a ticket
        // is available if the ticket is not canceled and it is 24 hours
        // prior to the scheduled flight and the scheduled flight has not
        // taken off already.
        [NotMapped]
        public bool BoardingPassAvailable
        {
            get
            {
                if (IsCanceled)
                {
                    return false;
                }

                var now = DateTime.Now;

                var oneDayBeforeDeparture = ScheduledFlight.GetDepartureTimestamp().AddDays(-1);

                return now >= oneDayBeforeDeparture && now < ScheduledFlight.GetDepartureTimestamp();
            }
        }

        // A computed property that simply inverts the BoardingPassAvailable
        // property for easier UI binding.
        [NotMapped]
        public bool NotBoardingPassAvailable => !BoardingPassAvailable;
    }
}
