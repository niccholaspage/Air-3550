using Air_3550.Models;
using Air_3550.Util;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class FlightSearchViewModel
    {
        public Airport OriginAirport;

        public Airport DestinationAirport;

        public DateTime Date;

        public ObservableCollection<FlightPath> Paths = new();

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
