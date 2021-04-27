// PaymentMethod.cs - Air 3550 Project
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
 * The PaymentMethod enumeration specifies
 * a payment method which can either be a
 * credit card, account balance, or points.
 * This is used by the Ticket to properly
 * handle refunds so that we can credit the
 * customer back properly.
 */

namespace Database.Models
{
    public enum PaymentMethod
    {
        CREDIT_CARD,
        ACCOUNT_BALANCE,
        POINTS
    }

    public static class Extensions
    {
        // A simple extension method allowing you to easily
        // get the nicely formatted string for a given payment
        // method for use in the user interface.
        public static string FormattedString(this PaymentMethod paymentMethod)
        {
            return paymentMethod switch
            {
                PaymentMethod.CREDIT_CARD => "Credit Card",
                PaymentMethod.ACCOUNT_BALANCE => "Account Balance",
                PaymentMethod.POINTS => "Points",
                _ => "Invalid Payment Method"
            };
        }
    }
}
