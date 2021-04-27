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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Air_3550.Models
{
    public class Airport
    {
        public int AirportId { get; set; }

        [Required]
        public string Code { get; set; }

        [Column(TypeName = "Decimal(8,6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "Decimal(9,6)")]
        public decimal Longitude { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; } // TODO: Determine this as well

        [NotMapped]
        public string CityWithState => City + ", " + State;

        [NotMapped]
        public string CityWithStateWithCode => CityWithState + " (" + Code + ")";
    }
}
