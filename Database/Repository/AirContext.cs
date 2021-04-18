﻿using Air_3550.Models;
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
