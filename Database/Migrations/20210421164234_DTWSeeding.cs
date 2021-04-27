// 20210421164234_DTWSeeding.cs - Air 3550 Project
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

using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Database.Migrations
{
    public partial class DTWSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 97, new TimeSpan(0, 7, 5, 0, 0), 7, false, 97, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 98, new TimeSpan(0, 10, 45, 0, 0), 7, false, 98, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 99, new TimeSpan(0, 13, 5, 0, 0), 7, false, 99, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 100, new TimeSpan(0, 16, 45, 0, 0), 7, false, 100, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 101, new TimeSpan(0, 10, 5, 0, 0), 3, false, 101, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 102, new TimeSpan(0, 13, 45, 0, 0), 3, false, 102, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 103, new TimeSpan(0, 16, 5, 0, 0), 3, false, 103, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 104, new TimeSpan(0, 19, 45, 0, 0), 3, false, 104, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 105, new TimeSpan(0, 16, 30, 0, 0), 9, false, 105, 3, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 106, new TimeSpan(0, 19, 0, 0, 0), 9, false, 106, 3, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 107, new TimeSpan(0, 22, 30, 0, 0), 9, false, 107, 3, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 108, new TimeSpan(1, 1, 0, 0, 0), 9, false, 108, 3, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 109, new TimeSpan(0, 13, 30, 0, 0), 3, false, 109, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 110, new TimeSpan(0, 16, 0, 0, 0), 3, false, 110, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 111, new TimeSpan(0, 19, 30, 0, 0), 3, false, 111, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 112, new TimeSpan(0, 22, 0, 0, 0), 3, false, 112, 9, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 112);
        }
    }
}
