using Air_3550.Controls;
using Air_3550.Repository;
using Air_3550.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Air_3550.Models;

namespace Air_3550.ViewModels
{
    class BookingsViewModel
    {
        public ObservableCollection<Booking> BookingsC = new();
        public ObservableCollection<Ticket> TicketsC = new();

        private readonly UserSessionService _userSessionService;

        public BookingsViewModel()
        {
            _userSessionService = App.Current.Services.GetService<UserSessionService>();
        }

        public async void GetBookings()
        {
            using (var db = new AirContext())
            {
                // TODO: How do we make this async?
                var bookings = await db.Bookings
                    .Where(Booking => Booking.UserId == _userSessionService.UserId)
                    .Include(Booking => Booking.Tickets)
                    .ThenInclude(Ticket => Ticket.ScheduledFlight)
                    .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                    .ThenInclude(Flight => Flight.OriginAirport)
                    .Include(Booking => Booking.Tickets)
                    .ThenInclude(Ticket => Ticket.ScheduledFlight)
                    .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                    .ThenInclude(Flight => Flight.DestinationAirport)
                    .ToListAsync();
                foreach (Booking a in bookings)
                {
                    BookingsC.Add(a);
                }

            }
        }
    }


}
