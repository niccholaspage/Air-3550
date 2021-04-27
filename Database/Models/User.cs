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

/**
 * The user model is used to represent
 * a user including their login information
 * as well as their role, which is used to
 * determine what they have access to in the
 * system.
 */

using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class User
    {
        public int UserId { get; set; } // The primary ID of the user, used to relate the user to other data.

        [Required]
        public string LoginId { get; set; } // The login ID or username of the user.

        // The password hash of the user, which is a Base64 encoded SHA512 hash of the user's password.
        [Required]
        public string PasswordHash { get; set; }

        // The role of the user, which determines what functionality they have access to.
        [Required]
        public Role Role { get; set; }
    }
}
