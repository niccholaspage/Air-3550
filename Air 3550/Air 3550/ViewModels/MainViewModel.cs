﻿using Air_3550.Util;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.ViewModels
{
    class MainViewModel : ObservableValidator
    {
        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        private int? _departureAirportId;

        [Required(ErrorMessage = "Please enter a valid departure city.")]
        public int? DepartureAirportId
        {
            get => _departureAirportId;
            set => SetProperty(ref _departureAirportId, value);
        }

        private int? _arrivalAirportId;

        [Required(ErrorMessage = "Please enter a valid arrival city.")]
        public int? ArrivalAirportId
        {
            get => _arrivalAirportId;
            set => SetProperty(ref _arrivalAirportId, value);
        }

        private DateTimeOffset? _departureDate;

        [Required(ErrorMessage = "Please enter a departure date.")]
        public DateTimeOffset? DepartureDate
        {
            get => _departureDate;
            set => SetProperty(ref _departureDate, value);
        }

        private DateTimeOffset? _returnDate;

        [Required(ErrorMessage = "Please enter a valid return date.")]
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
