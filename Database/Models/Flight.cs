using System;
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

        public double GetDistance()
        {
            static double toRadians(decimal angdeg)
            {
                return (double)angdeg / 180.0 * Math.PI;
            }

            const int R = 6371; // Radius of the earth

            double latDistance = toRadians(DestinationAirport.Latitude - OriginAirport.Latitude);
            double lonDistance = toRadians(DestinationAirport.Longitude - OriginAirport.Longitude);
            double a = Math.Sin(latDistance / 2) * Math.Sin(latDistance / 2)
                    + Math.Cos(toRadians(OriginAirport.Latitude)) * Math.Cos(toRadians(DestinationAirport.Latitude))
                    * Math.Sin(lonDistance / 2) * Math.Sin(lonDistance / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c * 1000; // convert to meters

            // Divide resulting height by 3.281 to convert feet to meters
            double height = (OriginAirport.Elevation - DestinationAirport.Elevation) / 3.281;

            distance = Math.Pow(distance, 2) + Math.Pow(height, 2);

            return Math.Sqrt(distance);
        }
    }
}
