using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        [Required]
        public ScheduledFlight ScheduledFlight { get; set; }

        public bool IsCanceled { get; set; }

        [Required]
        public Booking Booking { get; set; }
    }
}
