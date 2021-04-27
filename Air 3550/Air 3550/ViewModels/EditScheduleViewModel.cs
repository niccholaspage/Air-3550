using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Air_3550.ViewModels
{
    public record FlightWithDeletionActive(Flight Flight, bool DeletionActive);

    class EditScheduleViewModel : ObservableObject
    {
        private readonly UserSessionService userSessionService;

        public ObservableCollection<FlightWithDeletionActive> Flights = new();

        private bool _isLoadEngineer;

        public bool IsLoadEngineer
        {
            get => _isLoadEngineer;
            set => SetProperty(ref _isLoadEngineer, value);
        }

        public EditScheduleViewModel()
        {
            userSessionService = App.Current.Services.GetService<UserSessionService>();

            // If the marketing manager is on this page, we hide
            // the delete button because they are only allowed to
            // edit planes on flights and cannot delete a flight.
            IsLoadEngineer = userSessionService.Role == Role.LOAD_ENGINEER;
        }

        public async Task CancelFlight(FlightWithDeletionActive flight)
        {
            using (var db = new AirContext())
            {
                var lookupFlight = await db.Flights.FindAsync(flight.Flight.FlightId);

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

                foreach (Flight flight in queriedFlights)
                {
                    Flights.Add(new FlightWithDeletionActive(flight, IsLoadEngineer));
                }
            }
        }
    }
}
