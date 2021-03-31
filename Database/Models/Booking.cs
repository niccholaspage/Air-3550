using System.Collections.Generic;

namespace Air_3550.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
