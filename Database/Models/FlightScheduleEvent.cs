using System;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class FlightScheduleEvent
    {
        public int FlightScheduleEventId { get; set; }

        [Required]
        public TimeSpan FlightDepartureTime { get; set; }

        [Required]
        public Plane DefaultPlane { get; set; }

        public DateTime EndDate { get; set; }
    }
}
