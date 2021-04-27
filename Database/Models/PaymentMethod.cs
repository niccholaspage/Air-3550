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
