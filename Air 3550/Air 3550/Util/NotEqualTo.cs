// NotEqualTo.cs - Air 3550 Project
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
 * A validation attribute that checks if
 * the property the attribute is attached to
 * does not equal another property. If so,
 * validation is successful. Otherwise, it
 * returns the ValidationAttribute.ErrorMessage.
 */

using System.ComponentModel.DataAnnotations;

namespace Air_3550.Util
{
    class NotEqualTo : ValidationAttribute
    {
        private readonly string _otherProperty; // Store the name of the other property

        public NotEqualTo(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        // Here we override the ValidationAttribute.IsValid method
        // to check if our properties equal each other.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null) // If the value is not null,
            {
                // We grab the other property,
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(_otherProperty);

                // then retrieve the value of it.
                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance);

                // If the value of our own property equals the other property's value,
                if (value.Equals(otherPropertyValue))
                {
                    return new ValidationResult(ErrorMessage); // return an error.
                }
            }

            return ValidationResult.Success; // Otherwise, we were successful!
        }
    }
}
