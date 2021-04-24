using ABI.Microsoft.UI.Xaml;
using Air_3550.Models;
using Database.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Windows.Services.Maps;

namespace Air_3550.Util
{
    // TODO: Move this into another package.
    public class FlightPath
    {
        public List<Flight> Flights;

        private Lazy<string> _formattedDepartureTime;
        private Lazy<string> _formattedArrivalTime;
        private Lazy<decimal> _price;
        private Lazy<List<TimeSpan>> _flightDepartureTimeline;
        private Lazy<TimeSpan> _duration;

        public string FormattedDepartureTime => _formattedDepartureTime.Value;
        public string FormattedArrivalTime => _formattedArrivalTime.Value;
        public decimal Price => _price.Value;
        public TimeSpan Duration => _duration.Value;
        public List<TimeSpan> FlightDepartureTimeline => _flightDepartureTimeline.Value;

        public TimeSpan FirstFlightDepartureTime => Flights.First().DepartureTime;

        public string FirstFlightDepartureAirportCode => Flights.First().OriginAirport.Code;
        public string FirstFlightArrivalAirportCode => Flights.First().DestinationAirport.Code;
        public string LastFlightDepartureAirportCode => Flights.Last().OriginAirport.Code;
        public string LastFlightArrivalAirportCode => Flights.Last().DestinationAirport.Code;

        public FlightPath(params Flight[] flights)
        {
            Flights = new(flights);

            _formattedDepartureTime = new(() => DateTime.Today.Add(FirstFlightDepartureTime).ToString("h:mm tt"));
            _formattedArrivalTime = new(() => DateTime.Today.Add(Flights.Last().GetArrivalTime()).ToString("h:mm tt"));
            _price = new(() => Pricing.CalculatePriceOfFlights(Flights));

            _flightDepartureTimeline = new(() =>
            {
                var flightDepartureTimeline = new List<TimeSpan>();

                var timeSpan = new TimeSpan();

                // For the first flight, we just add its duration directly,
                // and its departure time is at 0 hours, 0 minutes, and 0 seconds.
                flightDepartureTimeline.Add(new TimeSpan(0, 0, 0));
                timeSpan += Flights[0].GetDuration();

                TimeSpan fourtyMinutes = new(0, 40, 0);

                TimeSpan oneDay = new(1, 0, 0, 0);

                for (int i = 1; i < Flights.Count; i++)
                {
                    var previousFlight = Flights[i - 1];
                    var flight = Flights[i];

                    TimeSpan layover;

                    if (flight.DepartureTime < previousFlight.GetArrivalTime().Add(fourtyMinutes))
                    {
                        // The flight departs before the previous flight arrives (plus the 40 minute minimum layover), so we
                        // need to proceed to the next day to determine the proper flight duration. Our layover is a day
                        // minus the difference of the previous flight's arrival time and the current flight's departure time.
                        layover = oneDay - (previousFlight.GetArrivalTime() - flight.DepartureTime);
                    }
                    else
                    {
                        layover = flight.DepartureTime - previousFlight.GetArrivalTime();
                    }

                    // For this flight, it's departure time occurs at our timespan + the layover.
                    flightDepartureTimeline.Add(timeSpan + layover);
                    timeSpan += layover + flight.GetDuration();
                }

                return flightDepartureTimeline;
            });

            _duration = new(() =>
            {
                // If we take the time the last flight departs in the timeline and
                // add it's duration, we get the total duration of the flight path.
                return FlightDepartureTimeline.Last() + Flights.Last().GetDuration();
            });
        }

        public TimeSpan MaxLayoverDuration
        {
            get
            {
                var maxLayoverDuration = new TimeSpan(0, 0, 0);

                for (int i = 1; i < FlightDepartureTimeline.Count; i++)
                {
                    var previousFlightDepartureTime = FlightDepartureTimeline[i - 1];
                    var currentFlightDepartureTime = FlightDepartureTimeline[i];

                    var layoverDuration = currentFlightDepartureTime - (previousFlightDepartureTime + Flights[i - 1].GetDuration());

                    if (maxLayoverDuration < layoverDuration)
                    {
                        maxLayoverDuration = layoverDuration;
                    }
                }

                return maxLayoverDuration;
            }
        }

        public int NumberOfStops => Flights.Count - 1;

        public string FormattedPrice => "$" + Price;

        public string FormattedDuration
        {
            get
            {
                var timeSpan = Duration;

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
    }
}
