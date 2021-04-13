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

        /**
         * Calculate distance between the origin airport and destination
         * airport. Returns the distance in miles.
         * 
         */
        public double GetDistance()
        {
            static double toRadians(decimal angdeg)
            {
                return (double)angdeg / 180.0 * Math.PI;
            }

            const int R = 6371; // Radius of the earth

            double latitudeDistance = toRadians(DestinationAirport.Latitude - OriginAirport.Latitude);
            double longitudeDistance = toRadians(DestinationAirport.Longitude - OriginAirport.Longitude);
            double a = Math.Sin(latitudeDistance / 2) * Math.Sin(latitudeDistance / 2)
                    + Math.Cos(toRadians(OriginAirport.Latitude)) * Math.Cos(toRadians(DestinationAirport.Latitude))
                    * Math.Sin(longitudeDistance / 2) * Math.Sin(longitudeDistance / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c * 1000; // convert to meters

            // Divide resulting height by 3.281 to convert feet to meters
            double height = (OriginAirport.Elevation - DestinationAirport.Elevation) / 3.281;

            distance = Math.Pow(distance, 2) + Math.Pow(height, 2);

            return Math.Sqrt(distance) / 1000 / 0.62137119; // We convert our distance we are about to return from meters to kilometers to miles.
        }

        // this calculates the duration of each flight
        public TimeSpan GetFlightDuration()
        {
            double permDuration = 30 + (60/500) * GetDistance();
            int hours = (int)permDuration / 60;
            int min = (int)permDuration % 60;
            TimeSpan duration = new TimeSpan(hours, min, 0);
            return duration;
        }
    }
}
