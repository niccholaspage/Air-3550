using System;

namespace Air_3550.ViewModels
{
    class MainViewModel : BindableBase
    {
        private string _departureCity;

        public string DepartureCity
        {
            get => _departureCity;
            set => Set(ref _departureCity, value);
        }

        private string _arrivalCity;

        public string ArrivalCity
        {
            get => _arrivalCity;
            set => Set(ref _arrivalCity, value);
        }

        private DateTimeOffset? _departureDate;

        public DateTimeOffset? DepartureDate
        {
            get => _departureDate;
            set => Set(ref _departureDate, value);
        }

        private DateTimeOffset? _arrivalDate;

        public DateTimeOffset? ArrivalDate
        {
            get => _arrivalDate;
            set => Set(ref _arrivalDate, value);
        }
    }
}
