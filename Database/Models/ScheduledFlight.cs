using System;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class ScheduledFlight
    {
        public int ScheduledFlightId { get; set; }

        [Required]
        public Flight Flight { get; set; }

        [Required]
        public DateTime DepartureTimestamp { get; set; }
    }
}
