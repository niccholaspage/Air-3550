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
                new Flight { FlightId = 26, Number = 26, OriginAirportId = 1, DestinationAirportId = 7, DepartureTime = new TimeSpan(20, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 27, Number = 27, OriginAirportId = 1, DestinationAirportId = 7, DepartureTime = new TimeSpan(23, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 28, Number = 28, OriginAirportId = 1, DestinationAirportId = 7, DepartureTime = new TimeSpan(02, 05, 00), PlaneId = 1 },

                // Flights from MDW to CLE
                new Flight { FlightId = 29, Number = 29, OriginAirportId = 7, DestinationAirportId = 1, DepartureTime = new TimeSpan(14, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 30, Number = 30, OriginAirportId = 7, DestinationAirportId = 1, DepartureTime = new TimeSpan(17, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 31, Number = 31, OriginAirportId = 7, DestinationAirportId = 1, DepartureTime = new TimeSpan(20, 05, 00), PlaneId = 1 },
                new Flight { FlightId = 32, Number = 32, OriginAirportId = 7, DestinationAirportId = 1, DepartureTime = new TimeSpan(23, 05, 00), PlaneId = 1 },

                // Flights from CLE to DEN
                new Flight { FlightId = 33, Number = 33, OriginAirportId = 1, DestinationAirportId = 9, DepartureTime = new TimeSpan(11, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 34, Number = 34, OriginAirportId = 1, DestinationAirportId = 9, DepartureTime = new TimeSpan(14, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 35, Number = 35, OriginAirportId = 1, DestinationAirportId = 9, DepartureTime = new TimeSpan(17, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 36, Number = 36, OriginAirportId = 1, DestinationAirportId = 9, DepartureTime = new TimeSpan(20, 15, 00), PlaneId = 2 },

                // Flights from DEN to CLE
                new Flight { FlightId = 37, Number = 37, OriginAirportId = 9, DestinationAirportId = 1, DepartureTime = new TimeSpan(14, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 38, Number = 38, OriginAirportId = 9, DestinationAirportId = 1, DepartureTime = new TimeSpan(17, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 39, Number = 39, OriginAirportId = 9, DestinationAirportId = 1, DepartureTime = new TimeSpan(20, 15, 00), PlaneId = 2 },
                new Flight { FlightId = 40, Number = 40, OriginAirportId = 9, DestinationAirportId = 1, DepartureTime = new TimeSpan(23, 15, 00), PlaneId = 2 }
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
