// MainViewModel.cs - Air 3550 Project
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

using Air_3550.Util;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.ViewModels
{
    class MainViewModel : ObservableValidator
    {
        private class RequiredIfRoundTrip : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var viewModel = validationContext.ObjectInstance as MainViewModel;

                if (viewModel.IsRoundTrip && value == null)
                {
                    return new ValidationResult(ErrorMessage);
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }

        private class GreaterThanDepartureDate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var departureDate = (validationContext.ObjectInstance as MainViewModel).DepartureDate;

                if (value != null && departureDate != null)
                {
                    var returnDate = (DateTimeOffset)value;

                    if (returnDate.Date <= departureDate.Value.Date)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }

                return ValidationResult.Success;
            }
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        private bool _isRoundTrip = true;

        public bool IsRoundTrip
        {
            get => _isRoundTrip;
            set => SetProperty(ref _isRoundTrip, value);
        }

        private int? _departureAirportId;

        [Required(ErrorMessage = "Please enter a valid departure city.")]
        public int? DepartureAirportId
        {
            get => _departureAirportId;
            set => SetProperty(ref _departureAirportId, value);
        }

        private int? _destinationAirportId;

        [Required(ErrorMessage = "Please enter a valid arrival city.")]
        [NotEqualTo(nameof(DepartureAirportId), ErrorMessage = "Departure and arrival cities cannot match.")]
        public int? DestinationAirportId
        {
            get => _destinationAirportId;
            set => SetProperty(ref _destinationAirportId, value);
        }

        private DateTimeOffset? _departureDate;

        [Required(ErrorMessage = "Please enter a departure date.")]
        public DateTimeOffset? DepartureDate
        {
            get => _departureDate;
            set => SetProperty(ref _departureDate, value);
        }

        private DateTimeOffset? _returnDate;

        [RequiredIfRoundTrip(ErrorMessage = "Please enter a valid return date.")]
        [GreaterThanDepartureDate(ErrorMessage = "Please enter a return date that is after your departure date.")]
        public DateTimeOffset? ReturnDate
        {
            get => _returnDate;
            set => SetProperty(ref _returnDate, value);
        }

        public bool CheckAndGiveFeedbackOnSearch()
        {
            ValidateAllProperties();

            // TODO: Wanna check dates here too?

            Feedback = this.GetFirstError();

            return !HasErrors;
        }
    }
}
