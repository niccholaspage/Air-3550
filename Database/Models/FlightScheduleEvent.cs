using System;

namespace Air_3550.Models
{
    public class FlightScheduleEvent
    {
        public int FlightScheduleEventId { get; set; }
        public TimeSpan FlightDepartureTime { get; set; }
        public Plane DefaultPlane { get; set; }
        public DateTime EndDate { get; set; }
    }
}
