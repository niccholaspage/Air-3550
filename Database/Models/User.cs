// User.cs - Air 3550 Project
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

using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string LoginId { get; set; }

        [Required]
        public string PasswordHash { get; set; } // TODO: Determine if this should be a string or not

        [Required]
        public Role Role { get; set; }
    }
}
