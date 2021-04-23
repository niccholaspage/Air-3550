using ABI.Microsoft.UI.Xaml;
using ABI.Microsoft.UI.Xaml.Controls;
using Air_3550.Models;
using Air_3550.Util;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class FlightSearchViewModel : ObservableObject
    {
        public Airport OriginAirport;

        public Airport DestinationAirport;

        public DateTime Date;

        public ObservableCollection<FlightPath> Paths = new();

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

        public async Task SearchForFlights(Airport originAirport, Airport destinationAirport, DateTime date)
        {
            OriginAirport = originAirport;
            DestinationAirport = destinationAirport;
            Date = date;

            var paths = await FlightSearchAlgorithm.FindFlightPaths(originAirport.AirportId, destinationAirport.AirportId);

            // TODO: This is cursed...
            foreach (var path in paths)
            {
                Paths.Add(path);
            }
        }
    }
}
