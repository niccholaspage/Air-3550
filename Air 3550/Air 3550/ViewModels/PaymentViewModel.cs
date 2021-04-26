using Air_3550.Repository;
using Air_3550.Services;
using Database.Util;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Air_3550.Models;
using Air_3550.Util;

namespace Air_3550.ViewModels
{
    class PaymentViewModel : ObservableObject
    {
        private readonly UserSessionService userSession;

        public PaymentViewModel()
        {
            userSession = App.Current.Services.GetService<UserSessionService>();

            Task.Run(FetchBalances);
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

        private decimal _accountBalance;

        public decimal AccountBalance
        {
            get => _accountBalance;
            set => SetProperty(ref _accountBalance, value);
        }

        private int _rewardPoints;

        public int RewardPoints
        {
            get => _rewardPoints;
            set => SetProperty(ref _rewardPoints, value);
        }

        public async void FetchBalances()
        {
            using (var db = new AirContext())
            {
                var accountBalance = await db.CustomerDatas
                    .Where(customerData => customerData.UserId == userSession.UserId)
                .Select(customerData => customerData.AccountBalance)
                .SingleAsync();

                AccountBalance = accountBalance;
                RewardPoints = await PointsHandler.UpdateAndRetrievePointsBalance(db);
            }
        }

        public decimal TotalCost => DepartingFlightPathWithDate.FlightPath.Price + (ReturnFlightPathWithDate != null ? ReturnFlightPathWithDate.FlightPath.Price : 0.0m);

        // A point corresponds to a single cent, so we
        // multiply the cost by 100 to get the total cost
        // in points.
        public int TotalCostInPoints => (int)(TotalCost * 100);

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
            var departureDate = flightPathWithDate.Date;

            for (int i = 0; i < flightPath.Flights.Count; i++)
            {
                var flight = flightPath.Flights[i];
                var flightDepartureTimeline = flightPath.FlightDepartureTimeline[i];

                var flightDepartureDate = (departureDate + flightDepartureTimeline).Date;
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
                    PaymentMethod = SelectedPaymentMethod
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
