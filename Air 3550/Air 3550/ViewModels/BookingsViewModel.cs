// BookingsViewModel.cs - Air 3550 Project
//
// This program emulates a flight reservation system for a new airline,
// allowing customers to book and manage trips,as well as allowing employees
// to update the available flights and view stats on them.
//
// Authors:		Nicholas Nassar, Jacob Hammitte, Nikesh Dhital
// Class:		EECS 3550-001 Software Engineering, Spring 2021
// Instructor:	Dr. Thomas
// Date:		April 28, 2021
// Copyright:	Copyright 2021 by Nicholas Nassar, Jacob Hammitte, and Nikesh Dhital. All rights reserved.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Services;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;

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
            set => SetProperty(ref _customerName, value);
        }

        private string _loginId;

        public string LoginId
        {
            get => _loginId;
            set => SetProperty(ref _loginId, value);
        }

        private Booking _selectedBooking;

        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set => SetProperty(ref _selectedBooking, value);
        }

        private Ticket _selectedTicket;

        public Ticket SelectedTicket
        {
            get => _selectedTicket;
            set => SetProperty(ref _selectedTicket, value);
        }

        public BookingsViewModel()
        {
            _userSessionService = App.Current.Services.GetService<UserSessionService>();
        }

        public async Task GetBookings()
        {
            Bookings.Clear();

            using var db = new AirContext();
            var customerInfo = await db.CustomerDatas
                .Where(customerData => customerData.CustomerDataId == _userSessionService.CustomerDataId)
                .Select(customerData => new { customerData.Name, customerData.User.LoginId })
            .SingleAsync();

            CustomerName = customerInfo.Name;
            LoginId = customerInfo.LoginId;

            var bookings = await db.Bookings
                .Include(booking => booking.Tickets)
                .ThenInclude(ticket => ticket.ScheduledFlight)
                .ThenInclude(scheduledFlight => scheduledFlight.Flight)
                .ThenInclude(flight => flight.OriginAirport)
                .Include(booking => booking.Tickets)
                .ThenInclude(ticket => ticket.ScheduledFlight)
                .ThenInclude(scheduledFlight => scheduledFlight.Flight)
                .ThenInclude(flight => flight.DestinationAirport)
                .Where(booking => booking.CustomerDataId == _userSessionService.CustomerDataId)
                .ToListAsync();
            foreach (Booking a in bookings.OrderBy(booking => booking.Tickets[0].ScheduledFlight.DepartureDate))
            {
                Bookings.Add(a);
            }
        }

        public async Task CancelBooking(Booking booking)
        {
            using (var db = new AirContext())
            {
                var canceledBooking = await db.Bookings
                        .Include(booking => booking.Tickets)
                        .ThenInclude(ticket => ticket.ScheduledFlight)
                        .ThenInclude(scheduledFlight => scheduledFlight.Flight)
                        .ThenInclude(flight => flight.OriginAirport)
                        .Include(booking => booking.Tickets)
                        .ThenInclude(ticket => ticket.ScheduledFlight)
                        .ThenInclude(scheduledFlight => scheduledFlight.Flight)
                        .ThenInclude(flight => flight.DestinationAirport)
                        .SingleAsync(booking => booking.BookingId == booking.BookingId);

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
