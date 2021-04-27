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

using Database.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        [Required]
        public ScheduledFlight ScheduledFlight { get; set; }

        public bool IsCanceled { get; set; }

        public bool PointsEarned { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public Booking Booking { get; set; }

        [NotMapped]
        public bool BoardingPassAvailable
        {
            get
            {
                var now = DateTime.Now;

                var oneDayBeforeDeparture = ScheduledFlight.GetDepartureTimestamp().AddDays(-1);

                return now >= oneDayBeforeDeparture && now < ScheduledFlight.GetDepartureTimestamp();
            }
        }

        [NotMapped]
        public bool NotBoardingPassAvailable => !(BoardingPassAvailable);
    }
}
