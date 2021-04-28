// ExtensionMethods.cs - Air 3550 Project
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
 * This class exists to add extension methods
 * to various classes. The reason this file
 * is contained in the Database project is due
 * to it's use in some of the database models.
 */

using System;

namespace Database.Util
{
    public static class ExtensionMethods
    {
        // First up, a simple method that takes a decimal
        // and formats it as money. for example,
        // 50.00 would be converted to $50.00.
        public static string FormatAsMoney(this decimal amount)
        {
            return string.Format("{0:C}", amount);
        }

        // A method that simply formats a DateTime into
        // a format as such: Sun, 04/19
        public static string FormatNicely(this DateTime dateTime) => dateTime.ToString("ddd, MM/dd");

        // A method that simply formats a TimeSpan into
        // a format like 4:09 PM.
        public static string FormatAsTimeNicely(this TimeSpan timeSpan)
        {
            return DateTime.Today.Add(timeSpan).ToString("h:mm tt");
        }

        // A method that formats a TimeSpan as a duration
        // as such: 1d 12h 3m. If a component is zero,
        // it is not included in the string.
        public static string FormatAsDurationNicely(this TimeSpan timeSpan)
        {
            string result = "";

            if (timeSpan.Days > 0)
            {
                result += timeSpan.Days + "d ";
            }

            if (timeSpan.Hours > 0)
            {
                result += timeSpan.Hours + "h ";
            }

            if (timeSpan.Minutes > 0)
            {
                result += timeSpan.Minutes + "m";
            }

            return result.Trim();
        }
    }
}
