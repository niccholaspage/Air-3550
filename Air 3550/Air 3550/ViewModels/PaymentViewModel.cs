// PaymentViewModel.cs - Air 3550 Project
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

/**
 * This view model handles the payment view,
 * and validates the user's payment method of
 * choice. It also has the purchasing logic,
 * converting a flight path into a proper set
 * of tickets for the customer, then creating
 * a booking to house them.
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Services;
using Air_3550.Util;
using Database.Models;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Air_3550.ViewModels
{
    class PaymentViewModel : ObservableObject
    {
        private readonly UserSessionService userSession;

        public PaymentViewModel()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();
        }

        private string _feedback;

        public string Feedback
        {
            get => _feedback;
            set => SetProperty(ref _feedback, value);
        }

        private FlightPathWithDate _departingFlightPathWithDate;

        public FlightPathWithDate DepartingFlightPathWithDate
        {
            get => _departingFlightPathWithDate;
            set => SetProperty(ref _departingFlightPathWithDate, value);
        }

        private FlightPathWithDate _returnFlightPathWithDate;

        public FlightPathWithDate ReturnFlightPathWithDate
        {
            get => _returnFlightPathWithDate;
            set => SetProperty(ref _returnFlightPathWithDate, value);
        }

        private PaymentMethod _selectedPaymentMethod;

        public PaymentMethod SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set => SetProperty(ref _selectedPaymentMethod, value);
        }

        private string _formattedAccountBalance;

        public string FormattedAccountBalance
        {
            get => _formattedAccountBalance;
            set => SetProperty(ref _formattedAccountBalance, value);
        }

        private int _formattedRewardPoints;

        public int FormattedRewardPoints
        {
            get => _formattedRewardPoints;
            set => SetProperty(ref _formattedRewardPoints, value);
        }

        public async Task FetchBalances()
        {
            using var db = new AirContext();
            var accountBalance = await db.CustomerDatas
                .Where(customerData => customerData.UserId == userSession.UserId)
            .Select(customerData => customerData.AccountBalance)
            .SingleAsync();

            FormattedAccountBalance = accountBalance.FormatAsMoney();

            var pointValues = await PointsHandler.UpdateAndRetrievePointsBalance(db);

            FormattedRewardPoints = pointValues.RewardPointsBalance;
        }

        public decimal TotalCost => DepartingFlightPathWithDate.FlightPath.Price + (ReturnFlightPathWithDate != null ? ReturnFlightPathWithDate.FlightPath.Price : 0.0m);

        public int TotalCostInPoints => DepartingFlightPathWithDate.FlightPath.PriceInPoints + (ReturnFlightPathWithDate != null ? ReturnFlightPathWithDate.FlightPath.PriceInPoints : 0);

        public bool IsReturnFlight => ReturnFlightPathWithDate != null;

        private async Task<List<Ticket>> CreateTicketsForFlightPath(AirContext db, FlightPathWithDate flightPathWithDate)
        {
            // We need to create tickets for a flight path. To do this,
            // we start from the date this flight path will be departing from,
            // and determine each day the flight will be departing from. We
            // try to find a scheduled flight for it, create one if it doesn't
            // exist, then make a ticket for it, then return a list of the tickets.
            List<Ticket> tickets = new();

            var flightPath = flightPathWithDate.FlightPath;
            var initialDepartureTimestamp = flightPathWithDate.FirstDepartureFlightTimestamp;

            // A ticket's price is based on the price of it's
            // flight, adding on the distributed fees across
            // this entire flight path. To do this, we first
            // get the price of the entire flight path:
            var flightPathCost = flightPath.Price;

            // And we get the variable cost of each flight:
            var variableFlightCosts = flightPath.Flights.Sum(flight => flight.GetCost());

            // We get the remaining cost:
            var remainingCost = flightPathCost - variableFlightCosts;

            // and get the distributed cost per flight:
            var distributedCost = remainingCost / flightPath.Flights.Count;

            for (int i = 0; i < flightPath.Flights.Count; i++)
            {
                var flight = flightPath.Flights[i];
                var flightDepartureTimeline = flightPath.FlightDepartureTimeline[i];

                var flightDepartureDate = (initialDepartureTimestamp + flightDepartureTimeline).Date;
                var scheduledFlight = await db.ScheduledFlights
                    .SingleOrDefaultAsync(scheduledFlight => scheduledFlight.Flight == flight && scheduledFlight.DepartureDate == flightDepartureDate);

                if (scheduledFlight == null)
                {
                    scheduledFlight = new ScheduledFlight
                    {
                        FlightId = flight.FlightId,
                        DepartureDate = flightDepartureDate
                    };
                };

                var ticket = new Ticket()
                {
                    ScheduledFlight = scheduledFlight,
                    PaymentMethod = SelectedPaymentMethod,
                    // and set the ticket's price to be it's
                    // flight cost + the distributed remaining
                    // cost
                    Price = flight.GetCost() + distributedCost
                };

                tickets.Add(ticket);
            }

            return tickets;
        }

        public async Task<bool> PurchaseTrip()
        {
            using (var db = new AirContext())
            {
                var customerDataBalances = await db.CustomerDatas
                    .Where(customerData => customerData.UserId == userSession.UserId)
                    .Select(customerData => new { customerData.RewardPointsBalance, customerData.AccountBalance })
                    .SingleAsync();

                if (SelectedPaymentMethod == PaymentMethod.CREDIT_CARD)
                {
                    Feedback = "Going with credit card, this is always valid.";
                }
                else if (SelectedPaymentMethod == PaymentMethod.ACCOUNT_BALANCE)
                {
                    if (customerDataBalances.AccountBalance < TotalCost)
                    {
                        Feedback = "You do not have enough money in your account balance.";

                        return false;
                    }
                }
                else
                {
                    if (customerDataBalances.RewardPointsBalance < TotalCostInPoints)
                    {
                        Feedback = "You do not have enough points.";

                        return false;
                    }
                }

                // TODO: Actually process the payment, deducting the necessary
                // account balance/reward points as needed.
                if (SelectedPaymentMethod == PaymentMethod.ACCOUNT_BALANCE)
                {
                    db.CustomerDatas.Find(userSession.CustomerDataId).AccountBalance -= TotalCost;
                    await db.SaveChangesAsync();
                }
                else if (SelectedPaymentMethod == PaymentMethod.POINTS)
                {
                    var customerData = db.CustomerDatas.Find(userSession.CustomerDataId);
                    customerData.RewardPointsBalance -= TotalCostInPoints;
                    customerData.RewardPointsUsed += TotalCostInPoints;
                    await db.SaveChangesAsync();
                }

                List<Ticket> tickets = await CreateTicketsForFlightPath(db, DepartingFlightPathWithDate);

                var booking = new Booking();

                if (ReturnFlightPathWithDate != null)
                {
                    booking.FirstReturnTicketIndex = tickets.Count;

                    tickets.AddRange(await CreateTicketsForFlightPath(db, ReturnFlightPathWithDate));
                }

                booking.Tickets.AddRange(tickets);

                booking.CustomerDataId = (int)userSession.CustomerDataId;

                await db.AddAsync(booking);

                await db.SaveChangesAsync();
            }


            Feedback = "Passed.";

            return true;
        }
    }
}
