using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class ScheduledFlight
    {
        public int ScheduledFlightId { get; set; }

        public int FlightId { get; set; }

        [Required]
        public Flight Flight { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        public List<Ticket> Tickets { get; }

        public DateTime GetDepartureTimestamp()
        {
            return DepartureDate.Add(Flight.DepartureTime);
        }

        public DateTime GetArrivalTimestamp()
        {
            return GetDepartureTimestamp().Add(Flight.GetDuration());
        }
    }
}
