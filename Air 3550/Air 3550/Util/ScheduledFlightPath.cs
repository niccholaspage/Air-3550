using Air_3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Air_3550.Util
{
    // TODO: Move this into another package.
    class ScheduledFlightPath
    {
        public List<ScheduledFlight> ScheduledFlights;

        public ScheduledFlightPath(List<ScheduledFlight> ScheduledFlights)
        {
            this.ScheduledFlights = ScheduledFlights;
        }

        public DateTime DepartureTime => ScheduledFlights.First().GetDepartureTimestamp();
        public DateTime ArrivalTime => ScheduledFlights.Last().GetArrivalTimestamp();
        public int NumberOfStops => ScheduledFlights.Count - 1;
    }
}
