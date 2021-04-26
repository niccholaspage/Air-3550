using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.Util
{
    class PointsHandler
    {
        public static async Task<int> UpdateAndRetrievePointsBalance(AirContext db)
        {
            var userSessionService = App.Current.Services.GetService<UserSessionService>();

            var customerData = await db.CustomerDatas
                .Include(customerData => customerData.Bookings)
                .ThenInclude(booking => booking.Tickets.Where(ticket => !ticket.PointsEarned && !ticket.IsCanceled))
                .ThenInclude(ticket => ticket.ScheduledFlight)
                .ThenInclude(scheduledFlight => scheduledFlight.Flight)
                .SingleOrDefaultAsync(customerData => customerData.CustomerDataId == userSessionService.CustomerDataId);

            int newPoints = 0;

            foreach (var booking in customerData.Bookings)
            {
                if (booking.HasFirstDepartureFlightDeparted())
                {
                    booking.GetDepartureTickets().ForEach(ticket => ticket.PointsEarned = true);
                    newPoints += (int)(booking.DepartureFlightPathWithDate.FlightPath.Price * 10);
                }

                if (booking.HasReturnTickets && booking.HasFirstReturnFlightDeparted())
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

            return customerData.RewardPointsBalance;
        }
    }
}
