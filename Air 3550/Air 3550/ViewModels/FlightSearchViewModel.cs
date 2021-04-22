using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class FlightSearchViewModel
    {
        public readonly Airport OriginAirport;

        public readonly Airport DestinationAirport;

        public ObservableCollection<ScheduledFlightPath> Paths = new();

        public FlightSearchViewModel()
        {
            // Temporarily populate for UI testing, clearly this is awful.
            using (var db = new AirContext())
            {
                if (db.ScheduledFlights.Count() == 3)
                {
                    OriginAirport = db.Airports.First();
                    DestinationAirport = db.Airports.ToList()[1];

                    var scheduledFlights = db.ScheduledFlights
                        .Include(scheduledFlight => scheduledFlight.Flight).ThenInclude(flight => flight.OriginAirport)
                        .Include(scheduledFlight => scheduledFlight.Flight).ThenInclude(flight => flight.DestinationAirport)
                        .ToList();

                    Paths.Add(new(new List<ScheduledFlight> { scheduledFlights[0] }));
                    Paths.Add(new(new List<ScheduledFlight> { scheduledFlights[1], scheduledFlights[2] }));
                }
            }
        }
    }
}
