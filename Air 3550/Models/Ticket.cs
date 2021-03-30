namespace Air_3550.Models
{
    class Ticket
    {
        public int TicketId { get; set; }
        public int ScheduledFlightId { get; set; }
        public ScheduledFlight ScheduledFlight { get; set; }
        public bool IsCanceled { get; set; }
    }
}
