using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Services;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.ViewModels
{
    class BookingsViewModel : ObservableObject
    {
        public ObservableCollection<Booking> Bookings = new();

        private readonly UserSessionService _userSessionService;

        private string _customerName;

        public string CustomerName
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
            Bookings.Clear();

            using (var db = new AirContext())
            {
                CustomerName = await db.CustomerDatas
                    .Where(customerData => customerData.CustomerDataId == _userSessionService.CustomerDataId)
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
                    Bookings.Add(a);
                }
            }
        }

        public async Task CancelBooking(Booking booking)
        {
            using (var db = new AirContext())
            {
                var canceledBooking = await db.Bookings
                        .Include(Booking => Booking.Tickets)
                        .ThenInclude(Ticket => Ticket.ScheduledFlight)
                        .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                        .ThenInclude(Flight => Flight.OriginAirport)
                        .Include(Booking => Booking.Tickets)
                        .ThenInclude(Ticket => Ticket.ScheduledFlight)
                        .ThenInclude(ScheduledFlight => ScheduledFlight.Flight)
                        .ThenInclude(Flight => Flight.DestinationAirport)
                        .SingleAsync(Booking => Booking.BookingId == booking.BookingId);

                List<Ticket> tickets;

                decimal refundedAmount;
                int refundedAmountInPoints;

                if (canceledBooking.CanCancelAllTickets)
                {
                    tickets = canceledBooking.Tickets;

                    refundedAmount = canceledBooking.DepartureFlightPathWithDate.FlightPath.Price;
                    refundedAmountInPoints = canceledBooking.DepartureFlightPathWithDate.FlightPath.PriceInPoints;

                    if (canceledBooking.HasReturnTickets)
                    {
                        refundedAmount += canceledBooking.ReturnFlightPathWithDate.FlightPath.Price;
                        refundedAmountInPoints += canceledBooking.ReturnFlightPathWithDate.FlightPath.PriceInPoints;
                    }
                }
                else
                {
                    tickets = canceledBooking.GetReturnTickets();
                    refundedAmount = canceledBooking.ReturnFlightPathWithDate.FlightPath.Price;
                    refundedAmountInPoints = canceledBooking.ReturnFlightPathWithDate.FlightPath.PriceInPoints;
                }

                foreach (Ticket ticket in tickets)
                {
                    ticket.IsCanceled = true;
                }

                var paymentMethod = tickets.First().PaymentMethod;

                var customer = await db.CustomerDatas.FindAsync(_userSessionService.CustomerDataId);

                if (paymentMethod == PaymentMethod.POINTS)
                {
                    // Refund to customer's points.
                    customer.RewardPointsBalance += refundedAmountInPoints;
                }
                else
                {
                    // Refund to account balance
                    customer.AccountBalance += refundedAmount;
                }

                await db.SaveChangesAsync();
            }

            await GetBookings();

        }
    }


}
