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
 * is contained in the Air 3550 project is due
 * to it's use of the MVVM toolkit from
 * Microsoft, which only the Air 3550 project
 * depends on.
 */

using System.Linq;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.Util
{
    public static class Extensions
    {
        // A method that simply takes an ObservableValidator
        // and returns the first error from it if one exists,
        // otherwise returning an empty string. Unfortunately,
        // WinUI does not support input validation on the
        // UI elements, so we couldn't show the user per-field
        // validation, so this is the next best thing - we simply
        // get the first error and show them it.
        public static string GetFirstError(this ObservableValidator validator)
        {
            // We get the errors from the validator, passing in null
            // as the property name (this returns errors for every property)
            // then we get the first one, or null if none exist.
            var firstError = validator.GetErrors(null).FirstOrDefault();

            // We then either return an empty string
            // or the error message depending on if
            // it exists.
            return firstError != null ? firstError.ErrorMessage : "";
        }
    }
}
