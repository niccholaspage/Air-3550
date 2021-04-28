// BoolToVisibilityConverter.cs - Air 3550 Project
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
 * A value converter used to convert a boolean value
 * to a Visibility for use with XAML bindings. By default,
 * the visibility of the value is true is visible, otherwise
 * it is collapsed. This can be configured by the consumer.
 */

using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace Air_3550.Converters
{
    class BoolToVisibilityConverter : IValueConverter
    {
        // Properties that determine which
        // visibility is seen based on the
        // value of the boolean.
        public Visibility OnTrue { get; set; }
        public Visibility OnFalse { get; set; }

        public BoolToVisibilityConverter()
        {
            // By default, a boolean that is
            // true results in visible, and a
            // boolean that is false results in
            // collapsed.
            OnTrue = Visibility.Visible;
            OnFalse = Visibility.Collapsed;
        }

        // This method simply converts the boolean
        // into the proper visibility, OnTrue or
        // OnFalse.
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            var boolean = (bool)value;

            return boolean ? OnTrue : OnFalse;
        }

        // This method simply converts a visibility
        // back into a boolean, true or false.
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
            {
                return visibility == OnTrue;
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
