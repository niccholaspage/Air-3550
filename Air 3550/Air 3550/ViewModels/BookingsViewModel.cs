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
using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class BookingsViewModel : ObservableValidator
    {
        public ObservableCollection<Booking> BookingsC = new();
        public ObservableCollection<Ticket> TicketsC = new();

        private readonly UserSessionService _userSessionService;

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

        public BookingsViewModel()
        {
            _userSessionService = App.Current.Services.GetService<UserSessionService>();
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        public async Task GetBookings()
        {
            //Grab all useful data related to a booking _userSessionService.CustomerDataId
            using (var db = new AirContext())
            {
                var bookings = await db.Bookings
                    .Include(Booking => Booking.Tickets)
                    .ThenInclude(Ticket => Ticket.ScheduledFlight)
                    .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                    .ThenInclude(Flight => Flight.OriginAirport)
                    .Include(Booking => Booking.Tickets)
                    .ThenInclude(Ticket => Ticket.ScheduledFlight)
                    .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                    .ThenInclude(Flight => Flight.DestinationAirport)
                    .Where(Booking => Booking.CustomerDataId == _userSessionService.CustomerDataId)
                    .ToListAsync();
                foreach (Booking a in bookings)
                {
                    BookingsC.Add(a);
                }
                Feedback = "" + BookingsC.Count;
            }
        }
    }


}
