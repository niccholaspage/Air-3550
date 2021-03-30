using System;

namespace Air_3550.Models
{
    class FlightScheduleEvent
    {
        public int FlightScheduleEventId { get; set; }
        public int FlightId { get; set; }
        public TimeSpan FlightDepartureTime { get; set; }

        public int DefaultPlaneId { get; set; }
        public Plane DefaultPlane { get; set; }

        public DateTime EndDate { get; set; }
    }
}
