using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace Air_3550.ViewModels
{
    class MainViewModel : ObservableObject
    {
        private int? _departureAirportId;

        public int? DepartureAirportId
        {
            get => _departureAirportId;
            set => SetProperty(ref _departureAirportId, value);
        }

        private int? _arrivalAirportId;

        public int? ArrivalAirportId
        {
            get => _arrivalAirportId;
            set => SetProperty(ref _arrivalAirportId, value);
        }

        private DateTimeOffset? _departureDate;

        public DateTimeOffset? DepartureDate
        {
            get => _departureDate;
            set => SetProperty(ref _departureDate, value);
        }

        private DateTimeOffset? _arrivalDate;

        public DateTimeOffset? ArrivalDate
        {
            get => _arrivalDate;
            set => SetProperty(ref _arrivalDate, value);
        }
    }
}
