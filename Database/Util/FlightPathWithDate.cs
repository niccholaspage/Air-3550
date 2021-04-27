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

using System;

namespace Database.Util
{
    public class FlightPathWithDate
    {
        private readonly FlightPath _flightPath;
        private readonly DateTime _date;

        public FlightPath FlightPath => _flightPath;
        public DateTime Date => _date;

        public string FormattedDate => _date.FormatNicely();

        public FlightPathWithDate(FlightPath flightPath, DateTime date)
        {
            _flightPath = flightPath;
            _date = date;
        }

        public DateTime FirstDepartureFlightTimestamp => _date + _flightPath.FirstFlightDepartureTime;

        public bool HasFirstFlightDeparted()
        {
            return DateTime.Now >= FirstDepartureFlightTimestamp;
        }
    }
}
