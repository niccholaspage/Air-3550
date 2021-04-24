using Air_3550.Models;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Flight> FlightsA = new();

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

        public async void CancelFlight(Flight cancelling)
        {
            using (var db = new AirContext())
            {
                var search = await db.Flights.SingleOrDefaultAsync(search => search.FlightId == cancelling.FlightId);
                search.IsCanceled = true;
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateFlights()
        {
            using (var db = new AirContext())
            {
                var FlightsT = await db.Flights
                    .Include(Flight => Flight.OriginAirport)
                    .Include(Flight => Flight.DestinationAirport)
                    .Where(f => f.IsCanceled == false)
                    .ToListAsync();
                foreach(Flight a in FlightsT)
                {
                    FlightsA.Add(a);
                }
            }
        }
    }
}
