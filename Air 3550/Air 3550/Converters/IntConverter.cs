// IntConverter.cs - Air 3550 Project
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
 * A value converter used to convert an integer value
 * to a string for use with XAML bindings.
 */

using System;
using Microsoft.UI.Xaml.Data;

namespace Air_3550.Converters
{
    class IntConverter : IValueConverter
    {
        // This method converts the value integer
        // into a string by simply calling it's
        // ToString method.
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }

            return value.ToString();
        }

        // This method attempts to convert back
        // the string by attempting to parse the
        // value then returning the number if the
        // parse succeeds.
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool parsed = int.TryParse(value.ToString(), out int number);

            return parsed ? number : null;
        }
    }
}
