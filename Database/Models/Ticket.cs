namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public ScheduledFlight ScheduledFlight { get; set; }
        public bool IsCanceled { get; set; }

        public Booking Booking { get; set; }
    }
}
