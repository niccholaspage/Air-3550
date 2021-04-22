using Air_3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Database.Util;

namespace Air_3550.Util
{
    // TODO: Move this into another package.
    class FlightPath
    {
        public List<Flight> Flights;

        public FlightPath(List<Flight> Flights)
        {
            this.Flights = Flights;
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
        public string FormattedPrice => "$" + Pricing.CalculatePriceOfFlights(Flights);
    }
}
