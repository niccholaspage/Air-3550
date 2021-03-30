namespace Air_3550.Models
{
    class Flight
    {
        public int FlightId { get; set; }
        public int Number { get; set; }

        public int OriginAirportId { get; set; }
        public Airport OriginAirport { get; set; }

        public int DestinationAirportId { get; set; }
        public Airport DestinationAirport { get; set; }
    }
}
