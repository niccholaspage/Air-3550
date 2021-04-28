// Airport.cs - Air 3550 Project
//
// This program emulates a flight reservation system for a new airline,
// allowing customers to book and manage trips,as well as allowing employees
// to update the available flights and view stats on them.
//
// Authors:		Nicholas Nassar, Jacob Hammitte, Nikesh Dhital
// Class:		EECS 3550-001 Software Engineering, Spring 2021
// Instructor:	Dr. Thomas
// Date:		April 28, 2021
// Copyright:	Copyright 2021 by Nicholas Nassar, Jacob Hammitte, and Nikesh Dhital. All rights reserved.

/**
 * The airport model is used to represent an
 * airport in the database with it's associated code,
 * latitude, longitude, city, and state.
 */

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Air_3550.Models
{
    public class Airport
    {
        public int AirportId { get; set; } // The primary ID of the airport, used to relate the airport to other data.

        [Required]
        public string Code { get; set; } // The airport's code, used in the UI to represent the code of an airport.

        // The airport's latitude. This is used with the longitude below to calculate the distance between two airports.
        [Column(TypeName = "Decimal(8,6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "Decimal(9,6)")]
        public decimal Longitude { get; set; } // The airport's longitude.

        [Required]
        public string City { get; set; } // The airport's city, used to show the user where an airport is located in. 

        [Required]
        public string State { get; set; } // The airport's state, used to show the user where an airport is located in. 

        [NotMapped]
        public string CityWithState => City + ", " + State; // A convenient computed property to return the city and state, nicely formatted.

        [NotMapped]
        public string CityWithStateWithCode => CityWithState + " (" + Code + ")"; // A conveinent computed property to return the city, state, and airport code, nicely formatted.
    }
}
