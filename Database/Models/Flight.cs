namespace Air_3550.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public int Number { get; set; }
        public Airport OriginAirport { get; set; }
        public Airport DestinationAirport { get; set; }
    }
}
