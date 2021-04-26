﻿using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditScheduleViewModel : ObservableValidator
    {
        public ObservableCollection<Flight> Flights = new();

        public async Task CancelFlight(Flight flight)
        {
            using (var db = new AirContext())
            {
                var lookupFlight = await db.Flights.FindAsync(flight.FlightId);

                lookupFlight.IsCanceled = true;

                await db.SaveChangesAsync();

                Flights.RemoveAt(Flights.IndexOf(flight));
            }
        }

        public async Task UpdateFlights()
        {
            using (var db = new AirContext())
            {
                Flights.Clear();

                var queriedFlights = await db.Flights
                    .Include(Flight => Flight.OriginAirport)
                    .Include(Flight => Flight.DestinationAirport)
                    .Where(flight => !flight.IsCanceled)
                    .ToListAsync();

                foreach (Flight a in queriedFlights)
                {
                    Flights.Add(a);
                }
            }
        }
    }
}
