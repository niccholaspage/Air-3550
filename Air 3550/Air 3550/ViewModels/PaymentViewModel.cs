﻿using Air_3550.Util;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class PaymentViewModel : ObservableObject
    {
        private FlightPath _departingFlightPath;
        private FlightPath _returnFlightPath;

        public FlightPath DepartingFlightPath
        {
            get => _departingFlightPath;
            set => SetProperty(ref _departingFlightPath, value);
        }
        public FlightPath ReturnFlightPath
        {
            get => _returnFlightPath;
            set => SetProperty(ref _returnFlightPath, value);
        }

        private decimal _totalCost;

        public decimal TotalCost
        {
            get => _totalCost;
            set => SetProperty(ref _totalCost, value);
        }

        public bool IsReturnFlight => ReturnFlightPath != null;
    }
}
