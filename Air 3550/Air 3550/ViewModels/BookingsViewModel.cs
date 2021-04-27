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
using Database.Models;

namespace Air_3550.ViewModels
{
    class BookingsViewModel : ObservableObject
    {
        public ObservableCollection<Booking> BookingsC = new();
        public ObservableCollection<Ticket> TicketsC = new();

        private readonly UserSessionService _userSessionService;

        private String _customerName;

        public String CustomerName
        {
            get => _customerName;
            set
            {
                SetProperty(ref _customerName, value);
            }
        }

        private Booking _selectedBooking;

        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                SetProperty(ref _selectedBooking, value);
            }
        }

        private Ticket _selectedTicket;

        public Ticket SelectedTicket
        {
            get => _selectedTicket;
            set
            {
                SetProperty(ref _selectedTicket, value);
            }
        }

        public BookingsViewModel()
        {
            _userSessionService = App.Current.Services.GetService<UserSessionService>();
        }

        public async Task GetBookings()
        {
            BookingsC.Clear();
            //Grab all useful data related to a booking _userSessionService.CustomerDataId
            using (var db = new AirContext())
            {
                CustomerName = await db.CustomerDatas
                    .Where(customerData => customerData.UserId == _userSessionService.UserId)
                    .Select(customerData => customerData.Name)
                .SingleAsync();

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
                //Feedback = "" + BookingsC.Count;
            }
        }

        public async Task cancelFlight(Booking cancelling)
        {
            decimal refund = 0;
            using (var db = new AirContext())
            {
                var Cancelling = await db.Bookings
                        .Include(Booking => Booking.Tickets)
                        .ThenInclude(Ticket => Ticket.ScheduledFlight)
                        .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                        .ThenInclude(Flight => Flight.OriginAirport)
                        .Include(Booking => Booking.Tickets)
                        .ThenInclude(Ticket => Ticket.ScheduledFlight)
                        .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                        .ThenInclude(Flight => Flight.DestinationAirport)
                        .Where(Booking => Booking.BookingId == cancelling.BookingId)
                        .SingleAsync();
                foreach(Ticket a in Cancelling.Tickets)
                {
                    a.IsCanceled = true;
                    if (a.PaymentMethod == PaymentMethod.POINTS)
                    {
                        //Point return
                    }
                    else
                    {
                        refund += a.ScheduledFlight.Flight.GetCost();
                    }
                }

                var customer = await db.CustomerDatas
                    .Where(customerData => customerData.UserId == _userSessionService.UserId)
                    .SingleAsync();

                customer.AccountBalance += refund;

                db.SaveChanges();
           }
            await GetBookings();

        }

        public async Task cancelReturnFlight(Booking cancelling)
        {
            decimal refund = 0;
            using (var db = new AirContext())
            {
                var Cancelling = await db.Bookings
                        .Include(Booking => Booking.Tickets)
                        .ThenInclude(Ticket => Ticket.ScheduledFlight)
                        .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                        .ThenInclude(Flight => Flight.OriginAirport)
                        .Include(Booking => Booking.Tickets)
                        .ThenInclude(Ticket => Ticket.ScheduledFlight)
                        .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                        .ThenInclude(Flight => Flight.DestinationAirport)
                        .Where(Booking => Booking.BookingId == cancelling.BookingId)
                        .SingleAsync();
                foreach (Ticket a in Cancelling.GetReturnTickets())
                {
                    a.IsCanceled = true;
                    if (a.PaymentMethod == PaymentMethod.POINTS)
                    {
                        //Point return
                    }
                    else
                    {
                        refund += a.ScheduledFlight.Flight.GetCost();
                    }
                }

                var customer = await db.CustomerDatas
                    .Where(customerData => customerData.UserId == _userSessionService.UserId)
                    .SingleAsync();

                customer.AccountBalance += refund;

                db.SaveChanges();
            }
            await GetBookings();

        }
    }


}
