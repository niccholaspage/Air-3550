// FlightPathWithDate.cs - Air 3550 Project
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
 * A simple class used to store and pass
 * around a flight path with a date. This
 * gets us a step closer to actually finding
 * or creating scheduled flights.
 */

using System;

namespace Database.Util
{
    public class FlightPathWithDate
    {
        public readonly FlightPath _flightPath;    // The flight path we are holding.
        public readonly DateTime _date;            // The date we are holding.

        // Properties to access the flight path and
        // date. This weirdness is needed because
        // binding (at least the legacy style)
        // requires properties and not fields.
        public FlightPath FlightPath => _flightPath;
        public DateTime Date => _date;

        public FlightPathWithDate(FlightPath flightPath, DateTime date)
        {
            _flightPath = flightPath;
            _date = date;
        }

        // A computed property to retrieve the formatted
        // date, for use with UI binding.
        public string FormattedDate => _date.FormatNicely();

        // A computed property to retrieve the first departure
        // flight timestamp for use in the determining ticket
        // cancellation in the booking.
        public DateTime FirstDepartureFlightTimestamp => _date + _flightPath.FirstFlightDepartureTime;

        // A method we use simply to check if the first flight
        // in a flight path with date has already departed.
        public bool HasFirstFlightDeparted()
        {
            // We simply see if the current time is equal to or has
            // passed the first departure flight timestamp.
            return DateTime.Now >= FirstDepartureFlightTimestamp;
        }
    }
}
