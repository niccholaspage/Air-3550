using Air_3550.Util;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

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

        public ObservableCollection<FlightPath> FlightPaths = new();

        private int _selectedPathIndex = -1;

        public int SelectedPathIndex
        {
            get => _selectedPathIndex;
            set
            {
                SetProperty(ref _selectedPathIndex, value);

                OnPropertyChanged(nameof(CanContinue));
            }
        }

        public bool CanContinue => SelectedPathIndex != -1;

        public bool IsreturnFlight => ReturnFlightPath != null;

        public void GrabPaths()
        {
            FlightPaths.Add(DepartingFlightPath);
            if (ReturnFlightPath != null) FlightPaths.Add(ReturnFlightPath);
        }
    }
}
