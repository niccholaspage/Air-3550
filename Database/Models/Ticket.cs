using Database.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        [Required]
        public ScheduledFlight ScheduledFlight { get; set; }

        public bool IsCanceled { get; set; }

        public bool PointsEarned { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public Booking Booking { get; set; }

        public bool BoardingPassAvailable => DateTime.Now >= ScheduledFlight.DepartureDate.Add((ScheduledFlight.GetDepartureTimestamp()).TimeOfDay).AddDays(-1);
        public bool NotBoardingPassAvailable => !(BoardingPassAvailable);
    }
}
