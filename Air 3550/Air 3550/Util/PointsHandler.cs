// PointsHandler.cs - Air 3550 Project
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
 * This static class simply handles
 * updating a customer's point balance
 * and retrieving it. It rewards a
 * customer points when the first departure
 * or return flight of a booking they have
 * not canceled takes off.
 */

using System.Linq;
using System.Threading.Tasks;
using Air_3550.Repository;
using Air_3550.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Air_3550.Util
{
    public static class PointsHandler
    {
        // We create a record to hold the points balance and total points used of a customer.
        public record PointValues(int RewardPointsBalance, int TotalRewardPointsUsed);

        // This method will retrieve the points balance from the database,
        // update date it due to bookings that have departed, then return
        // the updated values to the caller.
        public static async Task<PointValues> UpdateAndRetrievePointsBalance(AirContext db)
        {
            // We first retrieve the user session service
            // so we can get the ID of the customer data.
            var userSessionService = App.Current.Services.GetService<UserSessionService>();

            // We query for the customer data, including their bookings,
            // tickets, scheduled flights, flights, and airports.
            var customerData = await db.CustomerDatas
                .Include(customerData => customerData.Bookings)
                .ThenInclude(booking => booking.Tickets)
                .ThenInclude(ticket => ticket.ScheduledFlight)
                .ThenInclude(scheduledFlight => scheduledFlight.Flight)
                .ThenInclude(flight => flight.OriginAirport)
                .Include(customerData => customerData.Bookings)
                .ThenInclude(booking => booking.Tickets)
                .ThenInclude(ticket => ticket.ScheduledFlight)
                .ThenInclude(scheduledFlight => scheduledFlight.Flight)
                .ThenInclude(flight => flight.DestinationAirport)
                .SingleOrDefaultAsync(customerData => customerData.CustomerDataId == userSessionService.CustomerDataId);

            // We set a variable for the new points
            // we will be rewarding the customer.
            int newPoints = 0;

            foreach (var booking in customerData.Bookings) // Now we loop through each booking,
            {
                var departureTickets = booking.GetDepartureTickets();   // and grab the departure
                var returnTickets = booking.GetReturnTickets();         // and return tickets.

                /// If none of the departure tickets have been canceled and no points have been earned
                /// and the first departure flight has departed, we award the user points and flag the
                /// tickets as having their points earned.
                if (departureTickets.All(ticket => !ticket.IsCanceled && !ticket.PointsEarned) && booking.DepartureFlightPathWithDate.HasFirstFlightDeparted())
                {
                    booking.GetDepartureTickets().ForEach(ticket => ticket.PointsEarned = true);
                    newPoints += (int)(booking.DepartureFlightPathWithDate.FlightPath.Price * 10);
                }

                /// If none of the return tickets have been canceled and no points have been earned
                /// and the first return flight has departed, we award the user points and flag the
                /// tickets as having their points earned.
                if (booking.HasReturnTickets && returnTickets.All(ticket => !ticket.IsCanceled && !ticket.PointsEarned) && booking.ReturnFlightPathWithDate.HasFirstFlightDeparted())
                {
                    booking.GetReturnTickets().ForEach(ticket => ticket.PointsEarned = true);
                    newPoints += (int)(booking.ReturnFlightPathWithDate.FlightPath.Price * 10);
                }
            }

            // We then update the customer's reward points balance
            // if our new points are greater than zero, than save
            // to the database.
            if (newPoints > 0)
            {
                customerData.RewardPointsBalance += newPoints;

                await db.SaveChangesAsync();
            }

            // Finally, we return the point values to the caller so
            // they can do what they will with them, like display them
            // in the UI.
            return new PointValues(customerData.RewardPointsBalance, customerData.RewardPointsUsed);
        }
    }
}
