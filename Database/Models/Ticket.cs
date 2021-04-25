using Database.Models;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        [Required]
        public ScheduledFlight ScheduledFlight { get; set; }

        public bool IsCanceled { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public Booking Booking { get; set; }
    }
}
