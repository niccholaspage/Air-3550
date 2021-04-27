// 20210421193239_FinalFlightSeeding.cs - Air 3550 Project
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
    public partial class FinalFlightSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 210,
                column: "DepartureTime",
                value: new TimeSpan(0, 15, 20, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 212,
                column: "DepartureTime",
                value: new TimeSpan(0, 21, 20, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 214,
                column: "DepartureTime",
                value: new TimeSpan(0, 12, 20, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 216,
                column: "DepartureTime",
                value: new TimeSpan(0, 18, 20, 0, 0));

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 232, new TimeSpan(0, 21, 20, 0, 0), 9, false, 232, 10, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 231, new TimeSpan(0, 18, 50, 0, 0), 9, false, 231, 10, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 230, new TimeSpan(0, 15, 20, 0, 0), 9, false, 230, 10, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 229, new TimeSpan(0, 12, 50, 0, 0), 9, false, 229, 10, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 228, new TimeSpan(0, 0, 20, 0, 0), 10, false, 228, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 227, new TimeSpan(0, 21, 50, 0, 0), 10, false, 227, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 226, new TimeSpan(0, 18, 20, 0, 0), 10, false, 226, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 224, new TimeSpan(0, 19, 20, 0, 0), 8, false, 224, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 223, new TimeSpan(0, 16, 50, 0, 0), 8, false, 223, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 222, new TimeSpan(0, 13, 20, 0, 0), 8, false, 222, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 221, new TimeSpan(0, 10, 50, 0, 0), 8, false, 221, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 220, new TimeSpan(0, 22, 20, 0, 0), 9, false, 220, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 219, new TimeSpan(0, 19, 50, 0, 0), 9, false, 219, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 218, new TimeSpan(0, 16, 20, 0, 0), 9, false, 218, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 225, new TimeSpan(0, 15, 50, 0, 0), 10, false, 225, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 217, new TimeSpan(0, 13, 50, 0, 0), 9, false, 217, 8, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 232);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 210,
                column: "DepartureTime",
                value: new TimeSpan(0, 15, 50, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 212,
                column: "DepartureTime",
                value: new TimeSpan(0, 21, 50, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 214,
                column: "DepartureTime",
                value: new TimeSpan(0, 12, 50, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 216,
                column: "DepartureTime",
                value: new TimeSpan(0, 18, 50, 0, 0));
        }
    }
}
