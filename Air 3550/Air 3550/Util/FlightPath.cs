using Air_3550.Models;
using Database.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Air_3550.Util
{
    // TODO: Move this into another package.
    public class FlightPath
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
                var timeSpan = new TimeSpan();

                // For the first flight, we just add its duration directly.
                timeSpan += Flights[0].GetDuration();

                TimeSpan fourtyMinutes = new(0, 40, 0);

                for (int i = 1; i < Flights.Count; i++)
                {
                    var previousFlight = Flights[i - 1];
                    var flight = Flights[i];

                    if (flight.DepartureTime < previousFlight.GetArrivalTime().Add(fourtyMinutes))
                    {
                        // The flight departs before the previous flight arrives (plus the 40
                        // minute layovver), so we
                        // need to proceed to the next day to determine where the proper
                        // flight duration.
                        timeSpan += new TimeSpan(1, 0, 0, 0); // Add a day
                        timeSpan -= previousFlight.GetArrivalTime() - flight.DepartureTime; // Subtract the previous flight arrival time from the current flight's departure time
                    }
                    else
                    {
                        timeSpan += flight.DepartureTime - previousFlight.GetArrivalTime();
                    }

                    timeSpan += flight.GetDuration();
                }

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
