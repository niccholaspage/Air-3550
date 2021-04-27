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

/**
 * The customer data model is used to represent
 * a customer's data (attached to a specific user),
 * including identifiable information, like their name,
 * age, address, among other fields.
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class CustomerData
    {
        public int CustomerDataId { get; set; }  // The primary ID of the customer data, used to relate the customer data to other data.

        public int UserId { get; set; }  // The ID of the user this customer data is attached to.

        public User User { get; set; } // The user this customer data is attached to, since each customer data is for a specific user.

        [Required]
        public string Name { get; set; } // The full name of the customer.

        public int Age { get; set; } // The age of the customer.

        [Required]
        public string PhoneNumber { get; set; } // The phone number of the customer.

        [Required]
        public string Address { get; set; } // The address of the customer.

        [Required]
        public string ZipCode { get; set; } // The zip code of the customer.

        [Required]
        public string City { get; set; } // The city of the customer.

        [Required]
        public string State { get; set; } // The state of the customer.

        // Don't ever store it like this in a real app, but this is the credit card number of the user.
        [Required]
        public string CreditCardNumber { get; set; }

        // The account balance of the customer as a decimal to allow for cents.
        public decimal AccountBalance { get; set; }

        // The amount of points a customer currently has.
        public int RewardPointsBalance { get; set; }

        // The amount of points a customer has used in total.
        public int RewardPointsUsed { get; set; }

        // The list of bookings the customer has made.
        public List<Booking> Bookings { get; } = new();
    }
}
