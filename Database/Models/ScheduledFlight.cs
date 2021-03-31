using System;

namespace Air_3550.Models
{
    public class ScheduledFlight
    {
        public int ScheduledFlightId { get; set; }
        public Flight Flight { get; set; }
        public DateTime DepartureTimestamp { get; set; }
    }
}
