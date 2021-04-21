using Air_3550.Models;
using Database.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Air_3550.Repository
{
    public class AirContext : DbContext
    {
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CustomerData> CustomerDatas { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<ScheduledFlight> ScheduledFlights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Airport>().HasData(
                new Airport { AirportId = 1, Code = "CLE", City = "Cleveland", State = "Ohio", Latitude = 41.411667m, Longitude = -81.849722m, Elevation = 791 },
                new Airport { AirportId = 2, Code = "BNA", City = "Nashville", State = "Tennessee", Latitude = 36.126667m, Longitude = -86.681944m, Elevation = 599 },
                new Airport { AirportId = 3, Code = "DTW", City = "Detroit", State = "Michigan", Latitude = 42.2125m, Longitude = -83.353333m, Elevation = 645 },
                new Airport { AirportId = 4, Code = "ATL", City = "Atlanta", State = "Georgia", Latitude = 33.636667m, Longitude = -84.428056m, Elevation = 1026 },
                new Airport { AirportId = 5, Code = "LGA", City = "New York City", State = "New York", Latitude = 40.775m, Longitude = -73.875m, Elevation = 21 },
                new Airport { AirportId = 6, Code = "LAX", City = "Los Angeles", State = "California", Latitude = 33.9425m, Longitude = -118.408056m, Elevation = 128 },
                new Airport { AirportId = 7, Code = "MDW", City = "Chicago", State = "Illinois", Latitude = 41.786111m, Longitude = -87.7525m, Elevation = 668 },
                new Airport { AirportId = 8, Code = "DFW", City = "Dallas", State = "Texas", Latitude = 32.896944m, Longitude = -97.038056m, Elevation = 607 },
                new Airport { AirportId = 9, Code = "DEN", City = "Denver", State = "Colorado", Latitude = 39.861667m, Longitude = -104.673056m, Elevation = 5434 },
                new Airport { AirportId = 10, Code = "SEA", City = "Seattle", State = "Washington", Latitude = 47.448889m, Longitude = -122.309444m, Elevation = 433 }
                );

            modelBuilder.Entity<Plane>().HasData(
                new Plane { PlaneId = 1, Model = "Boeing 737 MAX", MaxSeats = 230, MaxDistance = 6570 },
                new Plane { PlaneId = 2, Model = "Boeing 747", MaxSeats = 416, MaxDistance = 14815 },
                new Plane { PlaneId = 3, Model = "Boeing 777", MaxSeats = 550, MaxDistance = 17395 }
                );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Role = Role.ACCOUNTANT, LoginId = "accountant", PasswordHash = PasswordHandling.HashPassword("accountant") },
                new User { UserId = 2, Role = Role.LOAD_ENGINEER, LoginId = "load_engineer", PasswordHash = PasswordHandling.HashPassword("load_engineer") },
                new User { UserId = 3, Role = Role.FLIGHT_MANAGER, LoginId = "flight_manager", PasswordHash = PasswordHandling.HashPassword("flight_manager") },
                new User { UserId = 4, Role = Role.MARKETING_MANAGER, LoginId = "marketing_manager", PasswordHash = PasswordHandling.HashPassword("marketing_manager") }
                );

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
                new Flight { FlightId = 105, Number = 105, OriginAirportId = 3, DestinationAirportId = 9, DepartureTime = new TimeSpan(16, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 106, Number = 106, OriginAirportId = 3, DestinationAirportId = 9, DepartureTime = new TimeSpan(19, 00, 00), PlaneId = 2 },
                new Flight { FlightId = 107, Number = 107, OriginAirportId = 3, DestinationAirportId = 9, DepartureTime = new TimeSpan(22, 30, 00), PlaneId = 2 },
                new Flight { FlightId = 108, Number = 108, OriginAirportId = 3, DestinationAirportId = 9, DepartureTime = new TimeSpan(25, 00, 00), PlaneId = 2 },

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
                new Flight { FlightId = 168, Number = 168, OriginAirportId = 9, DestinationAirportId = 5, DepartureTime = new TimeSpan(22, 30, 00), PlaneId = 3 }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var airDataDirectory = Path.Combine(appDataDirectory, "Air 3550 Team 4");
            Directory.CreateDirectory(airDataDirectory);
            var dbPath = Path.Combine(airDataDirectory, "air.db");
            options.UseSqlite(@"Data Source=" + dbPath);
        }
    }
}
