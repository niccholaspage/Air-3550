using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Air_3550.ViewModels
{
    class FlightSearchViewModel
    {
        public readonly Airport OriginAirport;

        public readonly Airport DestinationAirport;

        public ObservableCollection<FlightPath> Paths = new();

        public FlightSearchViewModel()
        {
            // Temporarily populate for UI testing, clearly this is awful.
            using (var db = new AirContext())
            {
                if (db.ScheduledFlights.Count() == 3)
                {
                    OriginAirport = db.Airports.First();
                    DestinationAirport = db.Airports.ToList()[1];

                    var flights = db.Flights.ToList();

                    Paths.Add(new(new List<Flight> { flights[0] }));
                    Paths.Add(new(new List<Flight> { flights[1], flights[2] }));
                }
            }
        }
    }
}
