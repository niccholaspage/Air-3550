using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.Util
{
    public class PointsHandler
    {
        public record PointValues(int RewardPointsBalance, int TotalRewardPointsUsed);

        public static async Task<PointValues> UpdateAndRetrievePointsBalance(AirContext db)
        {
            var userSessionService = App.Current.Services.GetService<UserSessionService>();

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

            int newPoints = 0;

            foreach (var booking in customerData.Bookings)
            {
                var departureTickets = booking.GetDepartureTickets();
                var returnTickets = booking.GetReturnTickets();

                if (departureTickets.All(ticket => !ticket.IsCanceled && !ticket.PointsEarned) && booking.DepartureFlightPathWithDate.HasFirstFlightDeparted())
                {
                    booking.GetDepartureTickets().ForEach(ticket => ticket.PointsEarned = true);
                    newPoints += (int)(booking.DepartureFlightPathWithDate.FlightPath.Price * 10);
                }

                if (booking.HasReturnTickets && returnTickets.All(ticket => !ticket.IsCanceled && !ticket.PointsEarned) && booking.ReturnFlightPathWithDate.HasFirstFlightDeparted())
                {
                    booking.GetReturnTickets().ForEach(ticket => ticket.PointsEarned = true);
                    newPoints += (int)(booking.ReturnFlightPathWithDate.FlightPath.Price * 10);
                }
            }

            if (newPoints > 0)
            {
                customerData.RewardPointsBalance += newPoints;

                await db.SaveChangesAsync();
            }

            return new PointValues(customerData.RewardPointsBalance, customerData.RewardPointsUsed);
        }
    }
}
