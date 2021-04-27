// ScheduledFlight.cs - Air 3550 Project
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

using Database.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Air_3550.Models
{
    public class ScheduledFlight
    {
        public int ScheduledFlightId { get; set; } // The primary ID of the scheduled flight, used to relate the scheduled flight to other data.

        public int FlightId { get; set; }

        [Required]
        public Flight Flight { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [NotMapped]
        public string FormattedDepartureDate => DepartureDate.FormatNicely();

        public List<Ticket> Tickets { get; }

        public DateTime GetDepartureTimestamp()
        {
            return DepartureDate.Add(Flight.DepartureTime);
        }

        public DateTime GetArrivalTimestamp()
        {
            return GetDepartureTimestamp().Add(Flight.GetDuration());
        }
    }
}
