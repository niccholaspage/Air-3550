// AirContext.cs - Air 3550 Project
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
 * The database context we will be using
 * across our entire application to access
 * the database with Entity Framework Core.
 */

using System;
using System.IO;
using Air_3550.Models;
using Database.Models;
using Database.Util;
using Microsoft.EntityFrameworkCore;

namespace Air_3550.Repository
{
    // This class extends EF Core's DBContext.
    // An instance of an AirContext represents
    // a session with the database and we use it
    // to query and save instances of our models.
    public class AirContext : DbContext
    {
        // Here, we declare the database sets for
        // each model in our database. This is very
        // similar to defining each table a database
        // will have in SQL.
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CustomerData> CustomerDatas { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<ScheduledFlight> ScheduledFlights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        // We override the model creating method
        // so that we can seed the database with all
        // of our data.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the method in the base class.

            // We first start with seeding our airport data. We seed
            // ten airports with all of their codes and locations.
            modelBuilder.Entity<Airport>().HasData(
                new Airport { AirportId = 1, Code = "CLE", City = "Cleveland", State = "Ohio", Latitude = 41.411667m, Longitude = -81.849722m },
                new Airport { AirportId = 2, Code = "BNA", City = "Nashville", State = "Tennessee", Latitude = 36.126667m, Longitude = -86.681944m },
                new Airport { AirportId = 3, Code = "DTW", City = "Detroit", State = "Michigan", Latitude = 42.2125m, Longitude = -83.353333m },
                new Airport { AirportId = 4, Code = "ATL", City = "Atlanta", State = "Georgia", Latitude = 33.636667m, Longitude = -84.428056m },
                new Airport { AirportId = 5, Code = "LGA", City = "New York City", State = "New York", Latitude = 40.775m, Longitude = -73.875m },
                new Airport { AirportId = 6, Code = "LAX", City = "Los Angeles", State = "California", Latitude = 33.9425m, Longitude = -118.408056m },
                new Airport { AirportId = 7, Code = "MDW", City = "Chicago", State = "Illinois", Latitude = 41.786111m, Longitude = -87.7525m },
                new Airport { AirportId = 8, Code = "DFW", City = "Dallas", State = "Texas", Latitude = 32.896944m, Longitude = -97.038056m },
                new Airport { AirportId = 9, Code = "DEN", City = "Denver", State = "Colorado", Latitude = 39.861667m, Longitude = -104.673056m },
                new Airport { AirportId = 10, Code = "SEA", City = "Seattle", State = "Washington", Latitude = 47.448889m, Longitude = -122.309444m }
                );

            // We then seed our plane data. We seed three planes,
            // with their model names, max seats, and maximum
            // distance.
            modelBuilder.Entity<Plane>().HasData(
                new Plane { PlaneId = 1, Model = "Boeing 737 MAX", MaxSeats = 230, MaxDistance = 6570 },
                new Plane { PlaneId = 2, Model = "Boeing 747", MaxSeats = 416, MaxDistance = 14815 },
                new Plane { PlaneId = 3, Model = "Boeing 777", MaxSeats = 550, MaxDistance = 17395 }
                );

            // We also seed our users. We seed an accountant, load engineer,
            // flight manager, and marketing manager. We also seed their passwords
            // to be the same as their login ID - we aren't going for security!
            // We also seed two dummy customers for initial data.
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Role = Role.ACCOUNTANT, LoginId = "accountant", PasswordHash = PasswordHandling.HashPassword("accountant") },
                new User { UserId = 2, Role = Role.LOAD_ENGINEER, LoginId = "load_engineer", PasswordHash = PasswordHandling.HashPassword("load_engineer") },
                new User { UserId = 3, Role = Role.FLIGHT_MANAGER, LoginId = "flight_manager", PasswordHash = PasswordHandling.HashPassword("flight_manager") },
                new User { UserId = 4, Role = Role.MARKETING_MANAGER, LoginId = "marketing_manager", PasswordHash = PasswordHandling.HashPassword("marketing_manager") },
                new User { UserId = 5, Role = Role.CUSTOMER, LoginId = "756967", PasswordHash = PasswordHandling.HashPassword("1234") },
                new User { UserId = 6, Role = Role.CUSTOMER, LoginId = "886642", PasswordHash = PasswordHandling.HashPassword("1234") }
                );

            // Finally, we seed our flights. For seeding, I
            // went to Southwest's site and searched for flights
            // going to and from each city. I took each flight,
            // created 3 copies of it and changed the departure time
            // for each one. I also based the plane model on the
            // distance between the two airports.
            modelBuilder.Entity<Flight>().HasData(
                // Flights from CLE to BNA
                new Flight { FlightId = 1, Number = 1, OriginAirportId = 1, DestinationAirportId = 2, DepartureTime = new TimeSpan(06, 35, 00), PlaneId = 1 },
                new Flight { FlightId = 2, Number = 2, OriginAirportId = 1, DestinationAirportId = 2, DepartureTime = new TimeSpan(09, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 3, Number = 3, OriginAirportId = 1, DestinationAirportId = 2, DepartureTime = new TimeSpan(12, 35, 00), PlaneId = 1 },
                new Flight { FlightId = 4, Number = 4, OriginAirportId = 1, DestinationAirportId = 2, DepartureTime = new TimeSpan(15, 30, 00), PlaneId = 1 },

                // Flights from BNA to CLE
                new Flight { FlightId = 5, Number = 5, OriginAirportId = 2, DestinationAirportId = 1, DepartureTime = new TimeSpan(09, 45, 00), PlaneId = 1 },
                new Flight { FlightId = 6, Number = 6, OriginAirportId = 2, DestinationAirportId = 1, DepartureTime = new TimeSpan(12, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 7, Number = 7, OriginAirportId = 2, DestinationAirportId = 1, DepartureTime = new TimeSpan(15, 45, 00), PlaneId = 1 },
                new Flight { FlightId = 8, Number = 8, OriginAirportId = 2, DestinationAirportId = 1, DepartureTime = new TimeSpan(18, 30, 00), PlaneId = 1 },

                // Flights from CLE to DTW
                new Flight { FlightId = 9, Number = 9, OriginAirportId = 1, DestinationAirportId = 3, DepartureTime = new TimeSpan(08, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 10, Number = 10, OriginAirportId = 1, DestinationAirportId = 3, DepartureTime = new TimeSpan(11, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 11, Number = 11, OriginAirportId = 1, DestinationAirportId = 3, DepartureTime = new TimeSpan(14, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 12, Number = 12, OriginAirportId = 1, DestinationAirportId = 3, DepartureTime = new TimeSpan(17, 15, 00), PlaneId = 1 },

                // Flights from DTW to CLE
                new Flight { FlightId = 13, Number = 13, OriginAirportId = 3, DestinationAirportId = 1, DepartureTime = new TimeSpan(11, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 14, Number = 14, OriginAirportId = 3, DestinationAirportId = 1, DepartureTime = new TimeSpan(14, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 15, Number = 15, OriginAirportId = 3, DestinationAirportId = 1, DepartureTime = new TimeSpan(17, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 16, Number = 16, OriginAirportId = 3, DestinationAirportId = 1, DepartureTime = new TimeSpan(20, 15, 00), PlaneId = 1 },

                // Flights from CLE to ATL
                new Flight { FlightId = 17, Number = 17, OriginAirportId = 1, DestinationAirportId = 4, DepartureTime = new TimeSpan(15, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 18, Number = 18, OriginAirportId = 1, DestinationAirportId = 4, DepartureTime = new TimeSpan(18, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 19, Number = 19, OriginAirportId = 1, DestinationAirportId = 4, DepartureTime = new TimeSpan(21, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 20, Number = 20, OriginAirportId = 1, DestinationAirportId = 4, DepartureTime = new TimeSpan(00, 15, 00), PlaneId = 1 },

                // Flights from ATL to CLE
                new Flight { FlightId = 21, Number = 21, OriginAirportId = 4, DestinationAirportId = 1, DepartureTime = new TimeSpan(12, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 22, Number = 22, OriginAirportId = 4, DestinationAirportId = 1, DepartureTime = new TimeSpan(15, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 23, Number = 23, OriginAirportId = 4, DestinationAirportId = 1, DepartureTime = new TimeSpan(18, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 24, Number = 24, OriginAirportId = 4, DestinationAirportId = 1, DepartureTime = new TimeSpan(21, 15, 00), PlaneId = 1 },

                // Flights from CLE to MDW
                new Flight { FlightId = 25, Number = 25, OriginAirportId = 1, DestinationAirportId = 7, DepartureTime = new TimeSpan(17, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 26, Number = 26, OriginAirportId = 1, DestinationAirportId = 7, DepartureTime = new TimeSpan(20, 35, 00), PlaneId = 1 },
                new Flight { FlightId = 27, Number = 27, OriginAirportId = 1, DestinationAirportId = 7, DepartureTime = new TimeSpan(23, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 28, Number = 28, OriginAirportId = 1, DestinationAirportId = 7, DepartureTime = new TimeSpan(02, 35, 00), PlaneId = 1 },

                // Flights from MDW to CLE
                new Flight { FlightId = 29, Number = 29, OriginAirportId = 7, DestinationAirportId = 1, DepartureTime = new TimeSpan(14, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 30, Number = 30, OriginAirportId = 7, DestinationAirportId = 1, DepartureTime = new TimeSpan(17, 35, 00), PlaneId = 1 },
                new Flight { FlightId = 31, Number = 31, OriginAirportId = 7, DestinationAirportId = 1, DepartureTime = new TimeSpan(20, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 32, Number = 32, OriginAirportId = 7, DestinationAirportId = 1, DepartureTime = new TimeSpan(23, 35, 00), PlaneId = 1 },

                // Flights from CLE to DEN
                new Flight { FlightId = 33, Number = 33, OriginAirportId = 1, DestinationAirportId = 9, DepartureTime = new TimeSpan(11, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 34, Number = 34, OriginAirportId = 1, DestinationAirportId = 9, DepartureTime = new TimeSpan(14, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 35, Number = 35, OriginAirportId = 1, DestinationAirportId = 9, DepartureTime = new TimeSpan(17, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 36, Number = 36, OriginAirportId = 1, DestinationAirportId = 9, DepartureTime = new TimeSpan(20, 45, 00), PlaneId = 2 },

                // Flights from DEN to CLE
                new Flight { FlightId = 37, Number = 37, OriginAirportId = 9, DestinationAirportId = 1, DepartureTime = new TimeSpan(14, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 38, Number = 38, OriginAirportId = 9, DestinationAirportId = 1, DepartureTime = new TimeSpan(17, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 39, Number = 39, OriginAirportId = 9, DestinationAirportId = 1, DepartureTime = new TimeSpan(20, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 40, Number = 40, OriginAirportId = 9, DestinationAirportId = 1, DepartureTime = new TimeSpan(23, 45, 00), PlaneId = 2 },

                // Flights from BNA to DTW
                new Flight { FlightId = 41, Number = 41, OriginAirportId = 2, DestinationAirportId = 3, DepartureTime = new TimeSpan(13, 40, 00), PlaneId = 1 },
                new Flight { FlightId = 42, Number = 42, OriginAirportId = 2, DestinationAirportId = 3, DepartureTime = new TimeSpan(16, 10, 00), PlaneId = 1 },
                new Flight { FlightId = 43, Number = 43, OriginAirportId = 2, DestinationAirportId = 3, DepartureTime = new TimeSpan(19, 40, 00), PlaneId = 1 },
                new Flight { FlightId = 44, Number = 44, OriginAirportId = 2, DestinationAirportId = 3, DepartureTime = new TimeSpan(22, 10, 00), PlaneId = 1 },

                // Flights from DTW to BNA
                new Flight { FlightId = 45, Number = 45, OriginAirportId = 3, DestinationAirportId = 2, DepartureTime = new TimeSpan(16, 40, 00), PlaneId = 1 },
                new Flight { FlightId = 46, Number = 46, OriginAirportId = 3, DestinationAirportId = 2, DepartureTime = new TimeSpan(19, 10, 00), PlaneId = 1 },
                new Flight { FlightId = 47, Number = 47, OriginAirportId = 3, DestinationAirportId = 2, DepartureTime = new TimeSpan(22, 40, 00), PlaneId = 1 },
                new Flight { FlightId = 48, Number = 48, OriginAirportId = 3, DestinationAirportId = 2, DepartureTime = new TimeSpan(01, 10, 00), PlaneId = 1 },

                // Flights from BNA to ATL
                new Flight { FlightId = 49, Number = 49, OriginAirportId = 2, DestinationAirportId = 4, DepartureTime = new TimeSpan(14, 00, 00), PlaneId = 1 },
                new Flight { FlightId = 50, Number = 50, OriginAirportId = 2, DestinationAirportId = 4, DepartureTime = new TimeSpan(17, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 51, Number = 51, OriginAirportId = 2, DestinationAirportId = 4, DepartureTime = new TimeSpan(20, 00, 00), PlaneId = 1 },
                new Flight { FlightId = 52, Number = 52, OriginAirportId = 2, DestinationAirportId = 4, DepartureTime = new TimeSpan(23, 30, 00), PlaneId = 1 },

                // Flights from ATL to BNA
                new Flight { FlightId = 53, Number = 53, OriginAirportId = 4, DestinationAirportId = 2, DepartureTime = new TimeSpan(11, 00, 00), PlaneId = 1 },
                new Flight { FlightId = 54, Number = 54, OriginAirportId = 4, DestinationAirportId = 2, DepartureTime = new TimeSpan(14, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 55, Number = 55, OriginAirportId = 4, DestinationAirportId = 2, DepartureTime = new TimeSpan(17, 00, 00), PlaneId = 1 },
                new Flight { FlightId = 56, Number = 56, OriginAirportId = 4, DestinationAirportId = 2, DepartureTime = new TimeSpan(20, 30, 00), PlaneId = 1 },

                // Flights from BNA to LGA
                new Flight { FlightId = 57, Number = 57, OriginAirportId = 2, DestinationAirportId = 5, DepartureTime = new TimeSpan(09, 45, 00), PlaneId = 1 },
                new Flight { FlightId = 58, Number = 58, OriginAirportId = 2, DestinationAirportId = 5, DepartureTime = new TimeSpan(12, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 59, Number = 59, OriginAirportId = 2, DestinationAirportId = 5, DepartureTime = new TimeSpan(15, 45, 00), PlaneId = 1 },
                new Flight { FlightId = 60, Number = 60, OriginAirportId = 2, DestinationAirportId = 5, DepartureTime = new TimeSpan(18, 15, 00), PlaneId = 1 },

                // Flights from LGA to BNA
                new Flight { FlightId = 61, Number = 61, OriginAirportId = 5, DestinationAirportId = 2, DepartureTime = new TimeSpan(06, 45, 00), PlaneId = 1 },
                new Flight { FlightId = 62, Number = 62, OriginAirportId = 5, DestinationAirportId = 2, DepartureTime = new TimeSpan(09, 15, 00), PlaneId = 1 },
                new Flight { FlightId = 63, Number = 63, OriginAirportId = 5, DestinationAirportId = 2, DepartureTime = new TimeSpan(12, 45, 00), PlaneId = 1 },
                new Flight { FlightId = 64, Number = 64, OriginAirportId = 5, DestinationAirportId = 2, DepartureTime = new TimeSpan(15, 15, 00), PlaneId = 1 },

                // Flights from BNA to LAX
                new Flight { FlightId = 65, Number = 65, OriginAirportId = 2, DestinationAirportId = 6, DepartureTime = new TimeSpan(06, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 66, Number = 66, OriginAirportId = 2, DestinationAirportId = 6, DepartureTime = new TimeSpan(09, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 67, Number = 67, OriginAirportId = 2, DestinationAirportId = 6, DepartureTime = new TimeSpan(12, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 68, Number = 68, OriginAirportId = 2, DestinationAirportId = 6, DepartureTime = new TimeSpan(15, 00, 00), PlaneId = 3 },

                // Flights from LAX to BNA
                new Flight { FlightId = 69, Number = 69, OriginAirportId = 6, DestinationAirportId = 2, DepartureTime = new TimeSpan(09, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 70, Number = 70, OriginAirportId = 6, DestinationAirportId = 2, DepartureTime = new TimeSpan(12, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 71, Number = 71, OriginAirportId = 6, DestinationAirportId = 2, DepartureTime = new TimeSpan(15, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 72, Number = 72, OriginAirportId = 6, DestinationAirportId = 2, DepartureTime = new TimeSpan(18, 00, 00), PlaneId = 3 },

                // Flights from BNA to MDW
                new Flight { FlightId = 73, Number = 73, OriginAirportId = 2, DestinationAirportId = 7, DepartureTime = new TimeSpan(06, 00, 00), PlaneId = 1 },
                new Flight { FlightId = 74, Number = 74, OriginAirportId = 2, DestinationAirportId = 7, DepartureTime = new TimeSpan(09, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 75, Number = 75, OriginAirportId = 2, DestinationAirportId = 7, DepartureTime = new TimeSpan(12, 00, 00), PlaneId = 1 },
                new Flight { FlightId = 76, Number = 76, OriginAirportId = 2, DestinationAirportId = 7, DepartureTime = new TimeSpan(15, 30, 00), PlaneId = 1 },

                // Flights from MDW to BNA
                new Flight { FlightId = 77, Number = 77, OriginAirportId = 7, DestinationAirportId = 2, DepartureTime = new TimeSpan(06, 00, 00), PlaneId = 1 },
                new Flight { FlightId = 78, Number = 78, OriginAirportId = 7, DestinationAirportId = 2, DepartureTime = new TimeSpan(09, 30, 00), PlaneId = 1 },
                new Flight { FlightId = 79, Number = 79, OriginAirportId = 7, DestinationAirportId = 2, DepartureTime = new TimeSpan(12, 00, 00), PlaneId = 1 },
                new Flight { FlightId = 80, Number = 80, OriginAirportId = 7, DestinationAirportId = 2, DepartureTime = new TimeSpan(15, 30, 00), PlaneId = 1 },

                // Flights from BNA to DFW
                new Flight { FlightId = 81, Number = 81, OriginAirportId = 2, DestinationAirportId = 8, DepartureTime = new TimeSpan(07, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 82, Number = 82, OriginAirportId = 2, DestinationAirportId = 8, DepartureTime = new TimeSpan(10, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 83, Number = 83, OriginAirportId = 2, DestinationAirportId = 8, DepartureTime = new TimeSpan(13, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 84, Number = 84, OriginAirportId = 2, DestinationAirportId = 8, DepartureTime = new TimeSpan(16, 45, 00), PlaneId = 2 },

                // Flights from DFW to BNA
                new Flight { FlightId = 85, Number = 85, OriginAirportId = 8, DestinationAirportId = 2, DepartureTime = new TimeSpan(10, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 86, Number = 86, OriginAirportId = 8, DestinationAirportId = 2, DepartureTime = new TimeSpan(13, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 87, Number = 87, OriginAirportId = 8, DestinationAirportId = 2, DepartureTime = new TimeSpan(16, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 88, Number = 88, OriginAirportId = 8, DestinationAirportId = 2, DepartureTime = new TimeSpan(19, 15, 00), PlaneId = 2 },

                // Flights from BNA to DEN
                new Flight { FlightId = 89, Number = 89, OriginAirportId = 2, DestinationAirportId = 9, DepartureTime = new TimeSpan(05, 40, 00), PlaneId = 2 },
                new Flight { FlightId = 90, Number = 90, OriginAirportId = 2, DestinationAirportId = 9, DepartureTime = new TimeSpan(08, 10, 00), PlaneId = 2 },
                new Flight { FlightId = 91, Number = 91, OriginAirportId = 2, DestinationAirportId = 9, DepartureTime = new TimeSpan(11, 40, 00), PlaneId = 2 },
                new Flight { FlightId = 92, Number = 92, OriginAirportId = 2, DestinationAirportId = 9, DepartureTime = new TimeSpan(14, 10, 00), PlaneId = 2 },

                // Flights from DEN to BNA
                new Flight { FlightId = 93, Number = 93, OriginAirportId = 9, DestinationAirportId = 2, DepartureTime = new TimeSpan(08, 40, 00), PlaneId = 2 },
                new Flight { FlightId = 94, Number = 94, OriginAirportId = 9, DestinationAirportId = 2, DepartureTime = new TimeSpan(11, 10, 00), PlaneId = 2 },
                new Flight { FlightId = 95, Number = 95, OriginAirportId = 9, DestinationAirportId = 2, DepartureTime = new TimeSpan(14, 40, 00), PlaneId = 2 },
                new Flight { FlightId = 96, Number = 96, OriginAirportId = 9, DestinationAirportId = 2, DepartureTime = new TimeSpan(17, 10, 00), PlaneId = 2 },

                // Flights from DTW to MDW
                new Flight { FlightId = 97, Number = 97, OriginAirportId = 3, DestinationAirportId = 7, DepartureTime = new TimeSpan(07, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 98, Number = 98, OriginAirportId = 3, DestinationAirportId = 7, DepartureTime = new TimeSpan(10, 45, 00), PlaneId = 1 },
                new Flight { FlightId = 99, Number = 99, OriginAirportId = 3, DestinationAirportId = 7, DepartureTime = new TimeSpan(13, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 100, Number = 100, OriginAirportId = 3, DestinationAirportId = 7, DepartureTime = new TimeSpan(16, 45, 00), PlaneId = 1 },

                // Flights from MDW to DTW
                new Flight { FlightId = 101, Number = 101, OriginAirportId = 7, DestinationAirportId = 3, DepartureTime = new TimeSpan(10, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 102, Number = 102, OriginAirportId = 7, DestinationAirportId = 3, DepartureTime = new TimeSpan(13, 45, 00), PlaneId = 1 },
                new Flight { FlightId = 103, Number = 103, OriginAirportId = 7, DestinationAirportId = 3, DepartureTime = new TimeSpan(16, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 104, Number = 104, OriginAirportId = 7, DestinationAirportId = 3, DepartureTime = new TimeSpan(19, 45, 00), PlaneId = 1 },

                // Flights from DTW to DEN
                new Flight { FlightId = 105, Number = 105, OriginAirportId = 3, DestinationAirportId = 9, DepartureTime = new TimeSpan(14, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 106, Number = 106, OriginAirportId = 3, DestinationAirportId = 9, DepartureTime = new TimeSpan(17, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 107, Number = 107, OriginAirportId = 3, DestinationAirportId = 9, DepartureTime = new TimeSpan(20, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 108, Number = 108, OriginAirportId = 3, DestinationAirportId = 9, DepartureTime = new TimeSpan(23, 00, 00), PlaneId = 2 },

                // Flights from DEN to DTW
                new Flight { FlightId = 109, Number = 109, OriginAirportId = 9, DestinationAirportId = 3, DepartureTime = new TimeSpan(13, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 110, Number = 110, OriginAirportId = 9, DestinationAirportId = 3, DepartureTime = new TimeSpan(16, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 111, Number = 111, OriginAirportId = 9, DestinationAirportId = 3, DepartureTime = new TimeSpan(19, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 112, Number = 112, OriginAirportId = 9, DestinationAirportId = 3, DepartureTime = new TimeSpan(22, 00, 00), PlaneId = 2 },

                // Flights from ATL to LGA
                new Flight { FlightId = 113, Number = 113, OriginAirportId = 4, DestinationAirportId = 5, DepartureTime = new TimeSpan(07, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 114, Number = 114, OriginAirportId = 4, DestinationAirportId = 5, DepartureTime = new TimeSpan(10, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 115, Number = 115, OriginAirportId = 4, DestinationAirportId = 5, DepartureTime = new TimeSpan(13, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 116, Number = 116, OriginAirportId = 4, DestinationAirportId = 5, DepartureTime = new TimeSpan(16, 30, 00), PlaneId = 2 },

                // Flights from LGA to ATL
                new Flight { FlightId = 117, Number = 117, OriginAirportId = 5, DestinationAirportId = 4, DepartureTime = new TimeSpan(10, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 118, Number = 118, OriginAirportId = 5, DestinationAirportId = 4, DepartureTime = new TimeSpan(13, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 119, Number = 119, OriginAirportId = 5, DestinationAirportId = 4, DepartureTime = new TimeSpan(16, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 120, Number = 120, OriginAirportId = 5, DestinationAirportId = 4, DepartureTime = new TimeSpan(19, 30, 00), PlaneId = 2 },

                // Flights from ATL to MDW
                new Flight { FlightId = 121, Number = 121, OriginAirportId = 4, DestinationAirportId = 7, DepartureTime = new TimeSpan(14, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 122, Number = 122, OriginAirportId = 4, DestinationAirportId = 7, DepartureTime = new TimeSpan(17, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 123, Number = 123, OriginAirportId = 4, DestinationAirportId = 7, DepartureTime = new TimeSpan(20, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 124, Number = 124, OriginAirportId = 4, DestinationAirportId = 7, DepartureTime = new TimeSpan(23, 15, 00), PlaneId = 2 },

                // Flights from MDW to ATL
                new Flight { FlightId = 125, Number = 125, OriginAirportId = 7, DestinationAirportId = 4, DepartureTime = new TimeSpan(14, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 126, Number = 126, OriginAirportId = 7, DestinationAirportId = 4, DepartureTime = new TimeSpan(17, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 127, Number = 127, OriginAirportId = 7, DestinationAirportId = 4, DepartureTime = new TimeSpan(20, 45, 00), PlaneId = 2 },
                new Flight { FlightId = 128, Number = 128, OriginAirportId = 7, DestinationAirportId = 4, DepartureTime = new TimeSpan(23, 15, 00), PlaneId = 2 },

                // Flights from ATL to DFW,
                new Flight { FlightId = 129, Number = 129, OriginAirportId = 4, DestinationAirportId = 8, DepartureTime = new TimeSpan(16, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 130, Number = 130, OriginAirportId = 4, DestinationAirportId = 8, DepartureTime = new TimeSpan(19, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 131, Number = 131, OriginAirportId = 4, DestinationAirportId = 8, DepartureTime = new TimeSpan(22, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 132, Number = 132, OriginAirportId = 4, DestinationAirportId = 8, DepartureTime = new TimeSpan(01, 30, 00), PlaneId = 2 },

                // Flights from DFW to ATL,
                new Flight { FlightId = 133, Number = 133, OriginAirportId = 8, DestinationAirportId = 4, DepartureTime = new TimeSpan(13, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 134, Number = 134, OriginAirportId = 8, DestinationAirportId = 4, DepartureTime = new TimeSpan(16, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 135, Number = 135, OriginAirportId = 8, DestinationAirportId = 4, DepartureTime = new TimeSpan(19, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 136, Number = 136, OriginAirportId = 8, DestinationAirportId = 4, DepartureTime = new TimeSpan(22, 30, 00), PlaneId = 2 },

                // Flights from ATL to DEN,
                new Flight { FlightId = 137, Number = 137, OriginAirportId = 4, DestinationAirportId = 9, DepartureTime = new TimeSpan(06, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 138, Number = 138, OriginAirportId = 4, DestinationAirportId = 9, DepartureTime = new TimeSpan(09, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 139, Number = 139, OriginAirportId = 4, DestinationAirportId = 9, DepartureTime = new TimeSpan(12, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 140, Number = 140, OriginAirportId = 4, DestinationAirportId = 9, DepartureTime = new TimeSpan(15, 00, 00), PlaneId = 3 },

                // Flights from DEN to ATL,
                new Flight { FlightId = 141, Number = 141, OriginAirportId = 9, DestinationAirportId = 4, DepartureTime = new TimeSpan(09, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 142, Number = 142, OriginAirportId = 9, DestinationAirportId = 4, DepartureTime = new TimeSpan(12, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 143, Number = 143, OriginAirportId = 9, DestinationAirportId = 4, DepartureTime = new TimeSpan(15, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 144, Number = 144, OriginAirportId = 9, DestinationAirportId = 4, DepartureTime = new TimeSpan(18, 00, 00), PlaneId = 3 },

                // Flights from LGA to MDW,
                new Flight { FlightId = 145, Number = 145, OriginAirportId = 5, DestinationAirportId = 7, DepartureTime = new TimeSpan(06, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 146, Number = 146, OriginAirportId = 5, DestinationAirportId = 7, DepartureTime = new TimeSpan(09, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 147, Number = 147, OriginAirportId = 5, DestinationAirportId = 7, DepartureTime = new TimeSpan(12, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 148, Number = 148, OriginAirportId = 5, DestinationAirportId = 7, DepartureTime = new TimeSpan(15, 00, 00), PlaneId = 2 },

                // Flights from MDW to LGA,
                new Flight { FlightId = 149, Number = 149, OriginAirportId = 7, DestinationAirportId = 5, DepartureTime = new TimeSpan(09, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 150, Number = 150, OriginAirportId = 7, DestinationAirportId = 5, DepartureTime = new TimeSpan(12, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 151, Number = 151, OriginAirportId = 7, DestinationAirportId = 5, DepartureTime = new TimeSpan(15, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 152, Number = 152, OriginAirportId = 7, DestinationAirportId = 5, DepartureTime = new TimeSpan(18, 00, 00), PlaneId = 2 },

                // Flights from LGA to DFW,
                new Flight { FlightId = 153, Number = 153, OriginAirportId = 5, DestinationAirportId = 8, DepartureTime = new TimeSpan(12, 50, 00), PlaneId = 3 },
                new Flight { FlightId = 154, Number = 154, OriginAirportId = 5, DestinationAirportId = 8, DepartureTime = new TimeSpan(15, 20, 00), PlaneId = 3 },
                new Flight { FlightId = 155, Number = 155, OriginAirportId = 5, DestinationAirportId = 8, DepartureTime = new TimeSpan(18, 50, 00), PlaneId = 3 },
                new Flight { FlightId = 156, Number = 156, OriginAirportId = 5, DestinationAirportId = 8, DepartureTime = new TimeSpan(21, 20, 00), PlaneId = 3 },

                // Flights from DFW to LGA,
                new Flight { FlightId = 157, Number = 157, OriginAirportId = 8, DestinationAirportId = 5, DepartureTime = new TimeSpan(9, 50, 00), PlaneId = 3 },
                new Flight { FlightId = 158, Number = 158, OriginAirportId = 8, DestinationAirportId = 5, DepartureTime = new TimeSpan(12, 20, 00), PlaneId = 3 },
                new Flight { FlightId = 159, Number = 159, OriginAirportId = 8, DestinationAirportId = 5, DepartureTime = new TimeSpan(15, 50, 00), PlaneId = 3 },
                new Flight { FlightId = 160, Number = 160, OriginAirportId = 8, DestinationAirportId = 5, DepartureTime = new TimeSpan(18, 20, 00), PlaneId = 3 },

                // Flights from LGA to DEN,
                new Flight { FlightId = 161, Number = 161, OriginAirportId = 5, DestinationAirportId = 9, DepartureTime = new TimeSpan(10, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 162, Number = 162, OriginAirportId = 5, DestinationAirportId = 9, DepartureTime = new TimeSpan(13, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 163, Number = 163, OriginAirportId = 5, DestinationAirportId = 9, DepartureTime = new TimeSpan(16, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 164, Number = 164, OriginAirportId = 5, DestinationAirportId = 9, DepartureTime = new TimeSpan(19, 30, 00), PlaneId = 3 },

                // Flights from DEN to LGA,
                new Flight { FlightId = 165, Number = 165, OriginAirportId = 9, DestinationAirportId = 5, DepartureTime = new TimeSpan(13, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 166, Number = 166, OriginAirportId = 9, DestinationAirportId = 5, DepartureTime = new TimeSpan(16, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 167, Number = 167, OriginAirportId = 9, DestinationAirportId = 5, DepartureTime = new TimeSpan(19, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 168, Number = 168, OriginAirportId = 9, DestinationAirportId = 5, DepartureTime = new TimeSpan(22, 30, 00), PlaneId = 3 },

                // Flights from LAX to MDW
                new Flight { FlightId = 169, Number = 169, OriginAirportId = 6, DestinationAirportId = 7, DepartureTime = new TimeSpan(09, 45, 00), PlaneId = 3 },
                new Flight { FlightId = 170, Number = 170, OriginAirportId = 6, DestinationAirportId = 7, DepartureTime = new TimeSpan(12, 15, 00), PlaneId = 3 },
                new Flight { FlightId = 171, Number = 171, OriginAirportId = 6, DestinationAirportId = 7, DepartureTime = new TimeSpan(15, 45, 00), PlaneId = 3 },
                new Flight { FlightId = 172, Number = 171, OriginAirportId = 6, DestinationAirportId = 7, DepartureTime = new TimeSpan(18, 15, 00), PlaneId = 3 },

                // Flights from MDW to LAX
                new Flight { FlightId = 173, Number = 173, OriginAirportId = 7, DestinationAirportId = 6, DepartureTime = new TimeSpan(12, 45, 00), PlaneId = 3 },
                new Flight { FlightId = 174, Number = 174, OriginAirportId = 7, DestinationAirportId = 6, DepartureTime = new TimeSpan(15, 15, 00), PlaneId = 3 },
                new Flight { FlightId = 175, Number = 175, OriginAirportId = 7, DestinationAirportId = 6, DepartureTime = new TimeSpan(18, 45, 00), PlaneId = 3 },
                new Flight { FlightId = 176, Number = 176, OriginAirportId = 7, DestinationAirportId = 6, DepartureTime = new TimeSpan(21, 15, 00), PlaneId = 3 },

                // Flights from LAX to DFW
                new Flight { FlightId = 177, Number = 177, OriginAirportId = 6, DestinationAirportId = 8, DepartureTime = new TimeSpan(17, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 178, Number = 178, OriginAirportId = 6, DestinationAirportId = 8, DepartureTime = new TimeSpan(20, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 179, Number = 179, OriginAirportId = 6, DestinationAirportId = 8, DepartureTime = new TimeSpan(23, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 180, Number = 180, OriginAirportId = 6, DestinationAirportId = 8, DepartureTime = new TimeSpan(02, 30, 00), PlaneId = 3 },

                // Flights from DFW to LAX
                new Flight { FlightId = 181, Number = 181, OriginAirportId = 8, DestinationAirportId = 6, DepartureTime = new TimeSpan(14, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 182, Number = 182, OriginAirportId = 8, DestinationAirportId = 6, DepartureTime = new TimeSpan(17, 30, 00), PlaneId = 3 },
                new Flight { FlightId = 183, Number = 183, OriginAirportId = 8, DestinationAirportId = 6, DepartureTime = new TimeSpan(20, 00, 00), PlaneId = 3 },
                new Flight { FlightId = 184, Number = 184, OriginAirportId = 8, DestinationAirportId = 6, DepartureTime = new TimeSpan(23, 30, 00), PlaneId = 3 },

                // Flights from LAX to DEN
                new Flight { FlightId = 185, Number = 185, OriginAirportId = 6, DestinationAirportId = 9, DepartureTime = new TimeSpan(09, 05, 00), PlaneId = 2 },
                new Flight { FlightId = 186, Number = 186, OriginAirportId = 6, DestinationAirportId = 9, DepartureTime = new TimeSpan(12, 35, 00), PlaneId = 2 },
                new Flight { FlightId = 187, Number = 187, OriginAirportId = 6, DestinationAirportId = 9, DepartureTime = new TimeSpan(15, 05, 00), PlaneId = 2 },
                new Flight { FlightId = 188, Number = 188, OriginAirportId = 6, DestinationAirportId = 9, DepartureTime = new TimeSpan(18, 35, 00), PlaneId = 2 },

                // Flights from DEN to LAX
                new Flight { FlightId = 189, Number = 189, OriginAirportId = 9, DestinationAirportId = 6, DepartureTime = new TimeSpan(12, 05, 00), PlaneId = 2 },
                new Flight { FlightId = 190, Number = 190, OriginAirportId = 9, DestinationAirportId = 6, DepartureTime = new TimeSpan(15, 35, 00), PlaneId = 2 },
                new Flight { FlightId = 191, Number = 191, OriginAirportId = 9, DestinationAirportId = 6, DepartureTime = new TimeSpan(18, 05, 00), PlaneId = 2 },
                new Flight { FlightId = 192, Number = 192, OriginAirportId = 9, DestinationAirportId = 6, DepartureTime = new TimeSpan(21, 35, 00), PlaneId = 2 },

                // Flights from MDW to DFW
                new Flight { FlightId = 193, Number = 193, OriginAirportId = 7, DestinationAirportId = 8, DepartureTime = new TimeSpan(04, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 194, Number = 194, OriginAirportId = 7, DestinationAirportId = 8, DepartureTime = new TimeSpan(07, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 195, Number = 195, OriginAirportId = 7, DestinationAirportId = 8, DepartureTime = new TimeSpan(10, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 196, Number = 196, OriginAirportId = 7, DestinationAirportId = 8, DepartureTime = new TimeSpan(13, 30, 00), PlaneId = 2 },

                // Flights from MDW to DFW
                new Flight { FlightId = 197, Number = 197, OriginAirportId = 8, DestinationAirportId = 7, DepartureTime = new TimeSpan(07, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 198, Number = 198, OriginAirportId = 8, DestinationAirportId = 7, DepartureTime = new TimeSpan(10, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 199, Number = 199, OriginAirportId = 8, DestinationAirportId = 7, DepartureTime = new TimeSpan(13, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 200, Number = 200, OriginAirportId = 8, DestinationAirportId = 7, DepartureTime = new TimeSpan(16, 30, 00), PlaneId = 2 },

                // Flights from MDW to DEN
                new Flight { FlightId = 201, Number = 201, OriginAirportId = 7, DestinationAirportId = 9, DepartureTime = new TimeSpan(12, 55, 00), PlaneId = 2 },
                new Flight { FlightId = 202, Number = 202, OriginAirportId = 7, DestinationAirportId = 9, DepartureTime = new TimeSpan(15, 25, 00), PlaneId = 2 },
                new Flight { FlightId = 203, Number = 203, OriginAirportId = 7, DestinationAirportId = 9, DepartureTime = new TimeSpan(18, 55, 00), PlaneId = 2 },
                new Flight { FlightId = 204, Number = 204, OriginAirportId = 7, DestinationAirportId = 9, DepartureTime = new TimeSpan(21, 25, 00), PlaneId = 2 },

                // Flights from DEN to MDW
                new Flight { FlightId = 205, Number = 205, OriginAirportId = 9, DestinationAirportId = 7, DepartureTime = new TimeSpan(12, 55, 00), PlaneId = 2 },
                new Flight { FlightId = 206, Number = 206, OriginAirportId = 9, DestinationAirportId = 7, DepartureTime = new TimeSpan(15, 25, 00), PlaneId = 2 },
                new Flight { FlightId = 207, Number = 207, OriginAirportId = 9, DestinationAirportId = 7, DepartureTime = new TimeSpan(18, 55, 00), PlaneId = 2 },
                new Flight { FlightId = 208, Number = 208, OriginAirportId = 9, DestinationAirportId = 7, DepartureTime = new TimeSpan(21, 25, 00), PlaneId = 2 },

                // Flights from MDW to SEA
                new Flight { FlightId = 209, Number = 209, OriginAirportId = 7, DestinationAirportId = 10, DepartureTime = new TimeSpan(12, 50, 00), PlaneId = 3 },
                new Flight { FlightId = 210, Number = 210, OriginAirportId = 7, DestinationAirportId = 10, DepartureTime = new TimeSpan(15, 20, 00), PlaneId = 3 },
                new Flight { FlightId = 211, Number = 211, OriginAirportId = 7, DestinationAirportId = 10, DepartureTime = new TimeSpan(18, 50, 00), PlaneId = 3 },
                new Flight { FlightId = 212, Number = 212, OriginAirportId = 7, DestinationAirportId = 10, DepartureTime = new TimeSpan(21, 20, 00), PlaneId = 3 },

                // Flights from SEA to MDW
                new Flight { FlightId = 213, Number = 213, OriginAirportId = 10, DestinationAirportId = 7, DepartureTime = new TimeSpan(09, 50, 00), PlaneId = 3 },
                new Flight { FlightId = 214, Number = 214, OriginAirportId = 10, DestinationAirportId = 7, DepartureTime = new TimeSpan(12, 20, 00), PlaneId = 3 },
                new Flight { FlightId = 215, Number = 215, OriginAirportId = 10, DestinationAirportId = 7, DepartureTime = new TimeSpan(15, 50, 00), PlaneId = 3 },
                new Flight { FlightId = 216, Number = 216, OriginAirportId = 10, DestinationAirportId = 7, DepartureTime = new TimeSpan(18, 20, 00), PlaneId = 3 },

                // Flights from DFW to DEN
                new Flight { FlightId = 217, Number = 217, OriginAirportId = 8, DestinationAirportId = 9, DepartureTime = new TimeSpan(13, 50, 00), PlaneId = 2 },
                new Flight { FlightId = 218, Number = 218, OriginAirportId = 8, DestinationAirportId = 9, DepartureTime = new TimeSpan(16, 20, 00), PlaneId = 2 },
                new Flight { FlightId = 219, Number = 219, OriginAirportId = 8, DestinationAirportId = 9, DepartureTime = new TimeSpan(19, 50, 00), PlaneId = 2 },
                new Flight { FlightId = 220, Number = 220, OriginAirportId = 8, DestinationAirportId = 9, DepartureTime = new TimeSpan(22, 20, 00), PlaneId = 2 },

                // Flights from DEN to DFW
                new Flight { FlightId = 221, Number = 221, OriginAirportId = 9, DestinationAirportId = 8, DepartureTime = new TimeSpan(10, 50, 00), PlaneId = 2 },
                new Flight { FlightId = 222, Number = 222, OriginAirportId = 9, DestinationAirportId = 8, DepartureTime = new TimeSpan(13, 20, 00), PlaneId = 2 },
                new Flight { FlightId = 223, Number = 223, OriginAirportId = 9, DestinationAirportId = 8, DepartureTime = new TimeSpan(16, 50, 00), PlaneId = 2 },
                new Flight { FlightId = 224, Number = 224, OriginAirportId = 9, DestinationAirportId = 8, DepartureTime = new TimeSpan(19, 20, 00), PlaneId = 2 },

                // Flights from DEN to SEA
                new Flight { FlightId = 225, Number = 225, OriginAirportId = 9, DestinationAirportId = 10, DepartureTime = new TimeSpan(15, 50, 00), PlaneId = 2 },
                new Flight { FlightId = 226, Number = 226, OriginAirportId = 9, DestinationAirportId = 10, DepartureTime = new TimeSpan(18, 20, 00), PlaneId = 2 },
                new Flight { FlightId = 227, Number = 227, OriginAirportId = 9, DestinationAirportId = 10, DepartureTime = new TimeSpan(21, 50, 00), PlaneId = 2 },
                new Flight { FlightId = 228, Number = 228, OriginAirportId = 9, DestinationAirportId = 10, DepartureTime = new TimeSpan(00, 20, 00), PlaneId = 2 },

                // Flights from SEA to DEN
                new Flight { FlightId = 229, Number = 229, OriginAirportId = 10, DestinationAirportId = 9, DepartureTime = new TimeSpan(12, 50, 00), PlaneId = 2 },
                new Flight { FlightId = 230, Number = 230, OriginAirportId = 10, DestinationAirportId = 9, DepartureTime = new TimeSpan(15, 20, 00), PlaneId = 2 },
                new Flight { FlightId = 231, Number = 231, OriginAirportId = 10, DestinationAirportId = 9, DepartureTime = new TimeSpan(18, 50, 00), PlaneId = 2 },
                new Flight { FlightId = 232, Number = 232, OriginAirportId = 10, DestinationAirportId = 9, DepartureTime = new TimeSpan(21, 20, 00), PlaneId = 2 }
                );

            // Now we need to seed the customer data for our two dummy users.
            modelBuilder.Entity<CustomerData>().HasData(
                new CustomerData { CustomerDataId = 1, UserId = 5, Name = "John Smith", Age = 39, PhoneNumber = "8492039944", Address = "3456 Pine Road", ZipCode = "28934", City = "South Wood", State = "Ohio", CreditCardNumber = "1234123412341234", AccountBalance = 0m, RewardPointsBalance = 0, RewardPointsUsed = 0 },
                new CustomerData { CustomerDataId = 2, UserId = 6, Name = "Steve Peterson", Age = 50, PhoneNumber = "3949907733", Address = "3459 Oak Road", ZipCode = "34564", City = "Farmington", State = "Ohio", CreditCardNumber = "234523452345322", AccountBalance = 252.56m, RewardPointsBalance = 0, RewardPointsUsed = 0 }
            );

            modelBuilder.Entity<ScheduledFlight>().HasData(
                new ScheduledFlight { ScheduledFlightId = 1, FlightId = 33, DepartureDate = new DateTime(2021, 05, 01) },
                new ScheduledFlight { ScheduledFlightId = 2, FlightId = 191, DepartureDate = new DateTime(2021, 05, 01) },
                new ScheduledFlight { ScheduledFlightId = 3, FlightId = 171, DepartureDate = new DateTime(2021, 05, 07) },
                new ScheduledFlight { ScheduledFlightId = 4, FlightId = 32, DepartureDate = new DateTime(2021, 05, 07) },
                new ScheduledFlight { ScheduledFlightId = 5, FlightId = 186, DepartureDate = new DateTime(2021, 05, 08) },
                new ScheduledFlight { ScheduledFlightId = 6, FlightId = 166, DepartureDate = new DateTime(2021, 05, 08) },
                new ScheduledFlight { ScheduledFlightId = 7, FlightId = 121, DepartureDate = new DateTime(2021, 05, 10) },
                new ScheduledFlight { ScheduledFlightId = 8, FlightId = 212, DepartureDate = new DateTime(2021, 05, 10) },
                new ScheduledFlight { ScheduledFlightId = 9, FlightId = 214, DepartureDate = new DateTime(2021, 05, 14) },
                new ScheduledFlight { ScheduledFlightId = 10, FlightId = 127, DepartureDate = new DateTime(2021, 05, 14) },
                new ScheduledFlight { ScheduledFlightId = 11, FlightId = 27, DepartureDate = new DateTime(2021, 04, 28) },
                new ScheduledFlight { ScheduledFlightId = 12, FlightId = 193, DepartureDate = new DateTime(2021, 04, 29) },
                new ScheduledFlight { ScheduledFlightId = 13, FlightId = 200, DepartureDate = new DateTime(2021, 04, 29) },
                new ScheduledFlight { ScheduledFlightId = 14, FlightId = 31, DepartureDate = new DateTime(2021, 04, 29) },
                new ScheduledFlight { ScheduledFlightId = 15, FlightId = 108, DepartureDate = new DateTime(2021, 04, 28) },
                new ScheduledFlight { ScheduledFlightId = 16, FlightId = 111, DepartureDate = new DateTime(2021, 04, 29) },
                new ScheduledFlight { ScheduledFlightId = 17, FlightId = 115, DepartureDate = new DateTime(2021, 05, 01) },
                new ScheduledFlight { ScheduledFlightId = 18, FlightId = 119, DepartureDate = new DateTime(2021, 05, 02) },
                new ScheduledFlight { ScheduledFlightId = 19, FlightId = 102, DepartureDate = new DateTime(2021, 05, 03) },
                new ScheduledFlight { ScheduledFlightId = 20, FlightId = 85, DepartureDate = new DateTime(2021, 05, 18) },
                new ScheduledFlight { ScheduledFlightId = 21, FlightId = 14, DepartureDate = new DateTime(2021, 05, 05) },
                new ScheduledFlight { ScheduledFlightId = 22, FlightId = 18, DepartureDate = new DateTime(2021, 05, 05) },
                new ScheduledFlight { ScheduledFlightId = 23, FlightId = 55, DepartureDate = new DateTime(2021, 05, 13) },
                new ScheduledFlight { ScheduledFlightId = 24, FlightId = 44, DepartureDate = new DateTime(2021, 05, 13) },
                new ScheduledFlight { ScheduledFlightId = 25, FlightId = 216, DepartureDate = new DateTime(2021, 05, 06) },
                new ScheduledFlight { ScheduledFlightId = 26, FlightId = 128, DepartureDate = new DateTime(2021, 05, 06) },
                new ScheduledFlight { ScheduledFlightId = 27, FlightId = 139, DepartureDate = new DateTime(2021, 05, 08) },
                new ScheduledFlight { ScheduledFlightId = 28, FlightId = 227, DepartureDate = new DateTime(2021, 05, 08) },
                new ScheduledFlight { ScheduledFlightId = 29, FlightId = 29, DepartureDate = new DateTime(2021, 05, 11) }
            );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { TicketId = 1, ScheduledFlightId = 1, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 1, Price = 170.4m },
                new Ticket { TicketId = 2, ScheduledFlightId = 2, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 1, Price = 119.55m },
                new Ticket { TicketId = 3, ScheduledFlightId = 3, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 1, Price = 212.87m },
                new Ticket { TicketId = 4, ScheduledFlightId = 4, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 1, Price = 53.4m },
                new Ticket { TicketId = 5, ScheduledFlightId = 5, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 2, Price = 129.89m },
                new Ticket { TicketId = 6, ScheduledFlightId = 6, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 2, Price = 201.14m },
                new Ticket { TicketId = 7, ScheduledFlightId = 7, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 3, Price = 95.06m },
                new Ticket { TicketId = 8, ScheduledFlightId = 8, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 3, Price = 190.14m },
                new Ticket { TicketId = 9, ScheduledFlightId = 9, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 3, Price = 234.17m },
                new Ticket { TicketId = 10, ScheduledFlightId = 10, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 3, Price = 90.45m },
                new Ticket { TicketId = 11, ScheduledFlightId = 11, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 4, Price = 58.4m },
                new Ticket { TicketId = 12, ScheduledFlightId = 12, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 4, Price = 105.62m },
                new Ticket { TicketId = 13, ScheduledFlightId = 13, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 4, Price = 122.28m },
                new Ticket { TicketId = 14, ScheduledFlightId = 14, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 4, Price = 59.58m },
                new Ticket { TicketId = 15, ScheduledFlightId = 15, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 5, Price = 147.62m },
                new Ticket { TicketId = 16, ScheduledFlightId = 16, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 5, Price = 166.07m },
                new Ticket { TicketId = 17, ScheduledFlightId = 17, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 6, Price = 141.4m },
                new Ticket { TicketId = 18, ScheduledFlightId = 18, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 6, Price = 141.4m },
                new Ticket { TicketId = 19, ScheduledFlightId = 19, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 7, Price = 77.36m },
                new Ticket { TicketId = 20, ScheduledFlightId = 20, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 8, Price = 125.68m },
                new Ticket { TicketId = 21, ScheduledFlightId = 21, IsCanceled = true, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 9, Price = 37.93m },
                new Ticket { TicketId = 22, ScheduledFlightId = 22, IsCanceled = true, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 9, Price = 86.54m },
                new Ticket { TicketId = 23, ScheduledFlightId = 23, IsCanceled = true, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 9, Price = 52.23m },
                new Ticket { TicketId = 24, ScheduledFlightId = 24, IsCanceled = true, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 9, Price = 75.86m },
                new Ticket { TicketId = 25, ScheduledFlightId = 25, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 10, Price = 210.9m },
                new Ticket { TicketId = 26, ScheduledFlightId = 26, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 10, Price = 80.84m },
                new Ticket { TicketId = 27, ScheduledFlightId = 27, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 10, Price = 167.81m },
                new Ticket { TicketId = 28, ScheduledFlightId = 28, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 10, Price = 122.23m },
                new Ticket { TicketId = 29, ScheduledFlightId = 29, IsCanceled = false, PointsEarned = false, PaymentMethod = PaymentMethod.CREDIT_CARD, BookingId = 11, Price = 86.76m }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking { BookingId = 1, CustomerDataId = 1, FirstReturnTicketIndex = 2 },
                new Booking { BookingId = 2, CustomerDataId = 1, FirstReturnTicketIndex = null },
                new Booking { BookingId = 3, CustomerDataId = 1, FirstReturnTicketIndex = 2 },
                new Booking { BookingId = 4, CustomerDataId = 1, FirstReturnTicketIndex = 2 },
                new Booking { BookingId = 5, CustomerDataId = 2, FirstReturnTicketIndex = 1 },
                new Booking { BookingId = 6, CustomerDataId = 2, FirstReturnTicketIndex = 1 },
                new Booking { BookingId = 7, CustomerDataId = 2, FirstReturnTicketIndex = null },
                new Booking { BookingId = 8, CustomerDataId = 2, FirstReturnTicketIndex = null },
                new Booking { BookingId = 9, CustomerDataId = 2, FirstReturnTicketIndex = 2 },
                new Booking { BookingId = 10, CustomerDataId = 2, FirstReturnTicketIndex = 2 },
                new Booking { BookingId = 11, CustomerDataId = 2, FirstReturnTicketIndex = null }
                );
        }

        // We also override the on configuring method,
        // which runs when the context is configured.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Before we get into setting our database file path, let's talk about
            // how packaged apps work. At the moment, WinUI 3 only supports packaged
            // apps. Packaged apps do not have direct access to the file system, so
            // the location of the database will actually end up at the following directory:
            // %localappdata%\Packages\13b1f18c-661a-4495-b3a2-861dd126199d_<user specific>\LocalCache\Roaming\Air 3550 Team 4

            // We get the roaming AppData path (again, this ends up different due to sandboxing)
            var appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // We get the directory for where we will store the database.
            var airDataDirectory = Path.Combine(appDataDirectory, "Air 3550 Team 4");

            Directory.CreateDirectory(airDataDirectory); // We create the directory incase it doesn't exist again.

            var databaseFilePath = Path.Combine(airDataDirectory, "air.db"); // Finally, we get the path to the file.

            // We then tell our database to use SQLite with our data source
            // set to the database file path we setup above.
            options.UseSqlite(@"Data Source=" + databaseFilePath);
        }
    }
}
