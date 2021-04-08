﻿// <auto-generated />
using System;
using Air_3550.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(AirContext))]
    [Migration("20210407224818_SeedPlanes")]
    partial class SeedPlanes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Air_3550.Models.Airport", b =>
                {
                    b.Property<int>("AirportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Elevation")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("Decimal(8,6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("Decimal(9,6)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AirportId");

                    b.ToTable("Airports");

                    b.HasData(
                        new
                        {
                            AirportId = 1,
                            City = "Cleveland",
                            Code = "CLE",
                            Elevation = 791,
                            Latitude = 41.411667m,
                            Longitude = -81.849722m,
                            State = "Ohio"
                        },
                        new
                        {
                            AirportId = 2,
                            City = "Nashville",
                            Code = "BNA",
                            Elevation = 599,
                            Latitude = 36.126667m,
                            Longitude = -86.681944m,
                            State = "Tennessee"
                        },
                        new
                        {
                            AirportId = 3,
                            City = "Detroit",
                            Code = "DTW",
                            Elevation = 645,
                            Latitude = 42.2125m,
                            Longitude = -83.353333m,
                            State = "Michigan"
                        },
                        new
                        {
                            AirportId = 4,
                            City = "Atlanta",
                            Code = "ATL",
                            Elevation = 1026,
                            Latitude = 33.636667m,
                            Longitude = -84.428056m,
                            State = "Georgia"
                        },
                        new
                        {
                            AirportId = 5,
                            City = "New York City",
                            Code = "JFK",
                            Elevation = 13,
                            Latitude = 40.639722m,
                            Longitude = -73.778889m,
                            State = "New York"
                        },
                        new
                        {
                            AirportId = 6,
                            City = "Los Angeles",
                            Code = "LAX",
                            Elevation = 128,
                            Latitude = 33.9425m,
                            Longitude = -118.408056m,
                            State = "California"
                        },
                        new
                        {
                            AirportId = 7,
                            City = "Chicago",
                            Code = "ORD",
                            Elevation = 668,
                            Latitude = 41.978611m,
                            Longitude = -87.904722m,
                            State = "Illinois"
                        },
                        new
                        {
                            AirportId = 8,
                            City = "Dallas",
                            Code = "DFW",
                            Elevation = 607,
                            Latitude = 32.896944m,
                            Longitude = -97.038056m,
                            State = "Ohio"
                        },
                        new
                        {
                            AirportId = 9,
                            City = "Denver",
                            Code = "DEN",
                            Elevation = 5434,
                            Latitude = 39.861667m,
                            Longitude = -104.673056m,
                            State = "Colorado"
                        },
                        new
                        {
                            AirportId = 10,
                            City = "Seattle",
                            Code = "SEA",
                            Elevation = 433,
                            Latitude = 47.448889m,
                            Longitude = -122.309444m,
                            State = "Washington"
                        });
                });

            modelBuilder.Entity("Air_3550.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CustomerDataId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BookingId");

                    b.HasIndex("CustomerDataId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Air_3550.Models.CustomerData", b =>
                {
                    b.Property<int>("CustomerDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountBalance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RewardPointsBalance")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RewardPointsUsed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerDataId");

                    b.HasIndex("UserId");

                    b.ToTable("CustomerDatas");
                });

            modelBuilder.Entity("Air_3550.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DestinationAirportAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OriginAirportAirportId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FlightId");

                    b.HasIndex("DestinationAirportAirportId");

                    b.HasIndex("OriginAirportAirportId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Air_3550.Models.FlightScheduleEvent", b =>
                {
                    b.Property<int>("FlightScheduleEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DefaultPlanePlaneId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("FlightDepartureTime")
                        .HasColumnType("TEXT");

                    b.HasKey("FlightScheduleEventId");

                    b.HasIndex("DefaultPlanePlaneId");

                    b.ToTable("FlightScheduleEvents");
                });

            modelBuilder.Entity("Air_3550.Models.Plane", b =>
                {
                    b.Property<int>("PlaneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PlaneId");

                    b.ToTable("Planes");

                    b.HasData(
                        new
                        {
                            PlaneId = 1,
                            MaxSeats = 230,
                            Model = "Boeing 737 MAX"
                        },
                        new
                        {
                            PlaneId = 2,
                            MaxSeats = 416,
                            Model = "Boeing 747"
                        },
                        new
                        {
                            PlaneId = 4,
                            MaxSeats = 550,
                            Model = "Boeing 777"
                        });
                });

            modelBuilder.Entity("Air_3550.Models.ScheduledFlight", b =>
                {
                    b.Property<int>("ScheduledFlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DepartureTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FlightId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScheduledFlightId");

                    b.HasIndex("FlightId");

                    b.ToTable("ScheduledFlights");
                });

            modelBuilder.Entity("Air_3550.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookingId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ScheduledFlightId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TicketId");

                    b.HasIndex("BookingId");

                    b.HasIndex("ScheduledFlightId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Air_3550.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LoginId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Air_3550.Models.Booking", b =>
                {
                    b.HasOne("Air_3550.Models.CustomerData", null)
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerDataId");
                });

            modelBuilder.Entity("Air_3550.Models.CustomerData", b =>
                {
                    b.HasOne("Air_3550.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Air_3550.Models.Flight", b =>
                {
                    b.HasOne("Air_3550.Models.Airport", "DestinationAirport")
                        .WithMany()
                        .HasForeignKey("DestinationAirportAirportId");

                    b.HasOne("Air_3550.Models.Airport", "OriginAirport")
                        .WithMany()
                        .HasForeignKey("OriginAirportAirportId");

                    b.Navigation("DestinationAirport");

                    b.Navigation("OriginAirport");
                });

            modelBuilder.Entity("Air_3550.Models.FlightScheduleEvent", b =>
                {
                    b.HasOne("Air_3550.Models.Plane", "DefaultPlane")
                        .WithMany()
                        .HasForeignKey("DefaultPlanePlaneId");

                    b.Navigation("DefaultPlane");
                });

            modelBuilder.Entity("Air_3550.Models.ScheduledFlight", b =>
                {
                    b.HasOne("Air_3550.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Air_3550.Models.Ticket", b =>
                {
                    b.HasOne("Air_3550.Models.Booking", "Booking")
                        .WithMany("Tickets")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Air_3550.Models.ScheduledFlight", "ScheduledFlight")
                        .WithMany()
                        .HasForeignKey("ScheduledFlightId");

                    b.Navigation("Booking");

                    b.Navigation("ScheduledFlight");
                });

            modelBuilder.Entity("Air_3550.Models.Booking", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Air_3550.Models.CustomerData", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}