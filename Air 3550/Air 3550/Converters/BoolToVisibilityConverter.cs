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

using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace Air_3550.Converters
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility OnTrue { get; set; }
        public Visibility OnFalse { get; set; }

        public BoolToVisibilityConverter()
        {
            OnTrue = Visibility.Visible;
            OnFalse = Visibility.Collapsed;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            var boolean = (bool)value;

            return boolean ? OnTrue : OnFalse;
        }

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
