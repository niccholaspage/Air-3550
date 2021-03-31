using System.ComponentModel.DataAnnotations.Schema;

namespace Air_3550.Models
{
    public class Airport
    {
        public int AirportId { get; set; }
        public string Code { get; set; }

        [Column(TypeName = "Decimal(8,6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "Decimal(9,6)")]
        public decimal Longitude { get; set; }

        public string City { get; set; }
        public string State { get; set; } // TODO: Determine this as well
    }
}
