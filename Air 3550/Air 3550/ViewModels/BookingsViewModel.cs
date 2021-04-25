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
using Air_3550.Util;
using System.Collections.Generic;
using Database.Util;

namespace Air_3550.ViewModels
{
    class BookingsViewModel
    {
        public ObservableCollection<FlightPathWithDate> BookingsC = new();
        public ObservableCollection<Ticket> TicketsC = new();

        private readonly UserSessionService _userSessionService;

        public BookingsViewModel()
        {
            _userSessionService = App.Current.Services.GetService<UserSessionService>();
        }

        public async void GetBookings()
        {
            //Grab all useful data related to a booking
            using (var db = new AirContext())
            {
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
                //Turns booking items into FlightPaths 
                List<Flight> adding = new List<Flight>();
                foreach (Booking a in bookings)
                {
                    foreach(Ticket b in a.Tickets)
                    {
                        adding.Add(b.ScheduledFlight.Flight);
                    }
                    BookingsC.Add(new FlightPathWithDate(new FlightPath(adding.ToArray()), a.Tickets.First().ScheduledFlight.DepartureDate));                
                }
            }
        }
    }


}
