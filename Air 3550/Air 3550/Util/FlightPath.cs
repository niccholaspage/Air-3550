using Air_3550.Models;
using Database.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Air_3550.Util
{
    // TODO: Move this into another package.
    class FlightPath
    {
        public List<Flight> Flights;

        public FlightPath(params Flight[] flights)
        {
            this.Flights = new(flights);
        }

        public string FormattedDepartureTime
        {
            get
            {
                return DateTime.Today.Add(Flights.First().DepartureTime).ToString("h:mm tt");
            }
        }

        public string FormattedArrivalTime
        {
            get
            {
                return DateTime.Today.Add(Flights.Last().GetArrivalTime()).ToString("h:mm tt");
            }
        }

        public int NumberOfStops => Flights.Count - 1;

        public string FormattedDuration
        {
            get
            {
                var timeSpan = Flights.Last().GetArrivalTime() - Flights.First().DepartureTime;

                string result = "";

                if (timeSpan.Days > 0)
                {
                    result += timeSpan.Days + "d ";
                }

                if (timeSpan.Hours > 0)
                {
                    result += timeSpan.Hours + "h ";
                }

                if (timeSpan.Minutes > 0)
                {
                    result += timeSpan.Minutes + "m";
                }

                return result.Trim();
            }
        }

        public string FormattedStops
        {
            get
            {
                return NumberOfStops switch
                {
                    0 => "Nonstop",
                    1 => NumberOfStops + " stop (" + Flights[0].DestinationAirport.Code + ")",
                    _ => NumberOfStops + " stops (" + Flights[0].DestinationAirport.Code + ", " + Flights[1].DestinationAirport.Code + ")"
                };
            }
        }

        public string FormattedPrice => "$" + Pricing.CalculatePriceOfFlights(Flights);
    }
}
