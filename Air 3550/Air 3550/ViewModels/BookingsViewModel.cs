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
        public ObservableCollection<Booking> BookingsC = new();
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
                    .Include(Booking => Booking.DepartureFlightPathWithDate)
                    .ThenInclude(FlightPathWithDate => FlightPathWithDate.FlightPath)
                    .ThenInclude(FlightPath => FlightPath.Flights)
                    .Include(Booking => Booking.ReturnFlightPathWithDate)
                    .ThenInclude(FlightPathWithDate => FlightPathWithDate.FlightPath)
                    .ThenInclude(FlightPath => FlightPath.Flights)
                    .ToListAsync();
                //Turns booking items into FlightPaths 
                foreach (Booking a in bookings)
                {
                    BookingsC.Append(a);
                }
            }
        }
    }


}
