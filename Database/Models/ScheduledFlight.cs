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

/**
 * The scheduled flight model is used to represent a
 * scheduled flight in the database. In our system, a
 * scheduled flight refers to a specific instance of a
 * flight occurring on a specific date. It does NOT
 * include any information that the flight model includes,
 * so it does have a refer to the flight for all of
 * that information.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Util;

namespace Air_3550.Models
{
    public class ScheduledFlight
    {
        public int ScheduledFlightId { get; set; } // The primary ID of the scheduled flight, used to relate the scheduled flight to other data.

        public int FlightId { get; set; } // The ID of the flight that this scheduled flight is based from.

        [Required]
        public Flight Flight { get; set; } // The flight that this scheduled flight is based from.

        // The departure date of this scheduled flight - this is
        // exclusively a date, so the time will always be midnight.
        [Required]
        public DateTime DepartureDate { get; set; }

        // A computed property formatting the departure date of the scheduled
        // flight nicely for UI bindings.
        [NotMapped]
        public string FormattedDepartureDate => DepartureDate.FormatNicely();

        // A computed property for the time and date of a flight's
        // departure formatted nicely for UI bindings.
        [NotMapped]
        public string FormattedDepartureDateWithTime => DepartureDate.FormatNicely() + " " + Flight.DepartureTime.FormatAsTimeNicely();

        // A computed property for the time and date of a flight's
        // return formatted nicely for UI bindings.
        [NotMapped]
        public string FormattedReturnDateWithTime => GetArrivalTimestamp().FormatNicely() + " " + Flight.GetArrivalTime().FormatAsTimeNicely();

        // A list of all the tickets that are attached to
        // this scheduled flight, which can be used for determining
        // how full this scheduled flight is.
        public List<Ticket> Tickets { get; }

        // A convenience method to return the exact timestamp
        // a scheduled flight departs by taking the departure date
        // and adding the base flight's departure time to it.
        public DateTime GetDepartureTimestamp()
        {
            return DepartureDate.Add(Flight.DepartureTime);
        }

        // A convenience method to return the exact timestamp
        // a scheduled flight arrives at by taking the departure
        // timestamp and adding the base flight's duration to it.
        public DateTime GetArrivalTimestamp()
        {
            return GetDepartureTimestamp().Add(Flight.GetDuration());
        }
    }
}
