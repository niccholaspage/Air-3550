using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public Airport OriginAirport { get; set; }

        [Required]
        public Airport DestinationAirport { get; set; }
    }
}
