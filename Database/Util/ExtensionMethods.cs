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

using System;

namespace Database.Util
{
    public static class ExtensionMethods
    {
        public static string FormatAsMoney(this decimal amount)
        {
            return string.Format("{0:C}", amount);
        }

        public static string FormatNicely(this DateTime dateTime) => dateTime.ToString("ddd, MM/dd");

        public static string FormatAsTimeNicely(this TimeSpan timeSpan)
        {
            return DateTime.Today.Add(timeSpan).ToString("h:mm tt");
        }

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
