using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_3550.Util
{
    class FlightSearchAlgorithm
    {
        public static async Task<List<FlightPath>> FindFlightPaths(int DepartureAirportId, int ArrivalAirportId)
        {
            using (var db = new AirContext())
            {
                var directFlights = await db.Flights
                    .Include(flight => flight.OriginAirport)
                    .Include(flight => flight.DestinationAirport)
                    .Where(flight => !flight.IsCanceled && flight.OriginAirportId == DepartureAirportId && flight.DestinationAirportId == ArrivalAirportId).ToListAsync();

                // If a direct flight exists, just return it outright.
                if (directFlights.Count != 0)
                {
                    return directFlights.Select(flight => new FlightPath(flight)).ToList();
                }

                var flights = db.Flights.Include(flight => flight.OriginAirport).Include(flight => flight.DestinationAirport).Where(flight => !flight.IsCanceled);

                // Since we only can have two connections possible and we have no direct flights,
                // let's first check for any two-legged possibilities.
                var query = from flight in flights
                            where flight.OriginAirportId == DepartureAirportId
                            join connection in flights on flight.DestinationAirportId equals connection.OriginAirportId
                            where connection.DestinationAirportId == ArrivalAirportId
                            select new FlightPath(flight, connection);

                return await query.ToListAsync();

                // TODO: Three legged
            }

            return new();
        }
    }
}
