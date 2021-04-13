using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace Air_3550.ViewModels
{
    class MainViewModel : ObservableObject
    {
        private string _departureCity;

        public string DepartureCity
        {
            get => _departureCity;
            set => SetProperty(ref _departureCity, value);
        }

        private string _arrivalCity;

        public string ArrivalCity
        {
            get => _arrivalCity;
            set => SetProperty(ref _arrivalCity, value);
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
