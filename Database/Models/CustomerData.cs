// CustomerData.cs - Air 3550 Project
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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class CustomerData
    {
        public int CustomerDataId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        public string PhoneNumber { get; set; } // determine format to put it into then save it here

        [Required]
        public string Address { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; } // Enumeration potentially? This will be a dropdown in UI.

        [Required]
        public string CreditCardNumber { get; set; } // Don't ever store it like this in a real app...

        public decimal AccountBalance { get; set; } // Store account balance as a decimal, make it look like dollars in the UI.

        public int RewardPointsBalance { get; set; }
        public int RewardPointsUsed { get; set; }

        public List<Booking> Bookings { get; } = new();
    }
}
