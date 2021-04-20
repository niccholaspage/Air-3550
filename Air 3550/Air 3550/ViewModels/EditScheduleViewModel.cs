using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class EditScheduleViewModel : ObservableValidator
    {
        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        private List<Flight> _flightsA;

        public List<Flight> FlightsA
        {
            get => _flightsA;
            set => SetProperty(ref _flightsA, value);
        }

        public async Task CancelFlight(Flight cancelling)
        {
            using (var db = new AirContext())
            {
                var search = await db.Flights.SingleOrDefaultAsync(search => search.FlightId == cancelling.FlightId);
                 search.IsCanceled = true;
                await db.SaveChangesAsync();
            }
        }

        public void UpdateFlights()
        {
            using (var db = new AirContext())
            {
                FlightsA = db.Flights
                    .Include(Flight => Flight.OriginAirport)
                    .Include(Flight => Flight.DestinationAirport)
                    .Where(f => f.IsCanceled == false)
                    .ToList();
            }
        }


    }
}
