// Plane.cs - Air 3550 Project
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

/*
 * The plane model is used to represent a plane
 * in the database. This model simply keeps track
 * of the the plane model, max seats, and max
 * distance the plane can travel.
 */

using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Plane
    {
        public int PlaneId { get; set; } // The primary ID of the plane, used to relate the airport to other data.

        [Required]
        public string Model { get; set; } // The model name of the plane.

        [Required]
        public int MaxSeats { get; set; } // The max number of seats on this plane.

        [Required]
        public int MaxDistance { get; set; } // The max distance this plane can travel.
    }
}
