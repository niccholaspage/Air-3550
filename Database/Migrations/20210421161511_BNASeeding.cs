// 20210421161511_BNASeeding.cs - Air 3550 Project
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
    public partial class BNASeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 26,
                column: "DepartureTime",
                value: new TimeSpan(0, 20, 35, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 28,
                column: "DepartureTime",
                value: new TimeSpan(0, 2, 35, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 30,
                column: "DepartureTime",
                value: new TimeSpan(0, 17, 35, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 32,
                column: "DepartureTime",
                value: new TimeSpan(0, 23, 35, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 34,
                column: "DepartureTime",
                value: new TimeSpan(0, 14, 45, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 36,
                column: "DepartureTime",
                value: new TimeSpan(0, 20, 45, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 38,
                column: "DepartureTime",
                value: new TimeSpan(0, 17, 45, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 40,
                column: "DepartureTime",
                value: new TimeSpan(0, 23, 45, 0, 0));

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 95, new TimeSpan(0, 14, 40, 0, 0), 2, false, 95, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 74, new TimeSpan(0, 9, 30, 0, 0), 7, false, 74, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 75, new TimeSpan(0, 12, 0, 0, 0), 7, false, 75, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 76, new TimeSpan(0, 15, 30, 0, 0), 7, false, 76, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 77, new TimeSpan(0, 6, 0, 0, 0), 2, false, 77, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 78, new TimeSpan(0, 9, 30, 0, 0), 2, false, 78, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 79, new TimeSpan(0, 12, 0, 0, 0), 2, false, 79, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 80, new TimeSpan(0, 15, 30, 0, 0), 2, false, 80, 7, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 81, new TimeSpan(0, 7, 45, 0, 0), 8, false, 81, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 82, new TimeSpan(0, 10, 15, 0, 0), 8, false, 82, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 83, new TimeSpan(0, 13, 45, 0, 0), 8, false, 83, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 84, new TimeSpan(0, 16, 45, 0, 0), 8, false, 84, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 85, new TimeSpan(0, 10, 45, 0, 0), 2, false, 85, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 86, new TimeSpan(0, 13, 15, 0, 0), 2, false, 86, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 87, new TimeSpan(0, 16, 45, 0, 0), 2, false, 87, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 73, new TimeSpan(0, 6, 0, 0, 0), 7, false, 73, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 89, new TimeSpan(0, 5, 40, 0, 0), 9, false, 89, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 90, new TimeSpan(0, 8, 10, 0, 0), 9, false, 90, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 91, new TimeSpan(0, 11, 40, 0, 0), 9, false, 91, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 92, new TimeSpan(0, 14, 10, 0, 0), 9, false, 92, 2, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 93, new TimeSpan(0, 8, 40, 0, 0), 2, false, 93, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 94, new TimeSpan(0, 11, 10, 0, 0), 2, false, 94, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 96, new TimeSpan(0, 17, 10, 0, 0), 2, false, 96, 9, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 88, new TimeSpan(0, 19, 15, 0, 0), 2, false, 88, 8, 2 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 72, new TimeSpan(0, 18, 0, 0, 0), 2, false, 72, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 70, new TimeSpan(0, 12, 0, 0, 0), 2, false, 70, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 42, new TimeSpan(0, 16, 10, 0, 0), 3, false, 42, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 43, new TimeSpan(0, 19, 40, 0, 0), 3, false, 43, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 44, new TimeSpan(0, 22, 10, 0, 0), 3, false, 44, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 45, new TimeSpan(0, 16, 40, 0, 0), 2, false, 45, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 46, new TimeSpan(0, 19, 10, 0, 0), 2, false, 46, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 47, new TimeSpan(0, 22, 40, 0, 0), 2, false, 47, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 48, new TimeSpan(0, 1, 10, 0, 0), 2, false, 48, 3, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 49, new TimeSpan(0, 14, 0, 0, 0), 4, false, 49, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 50, new TimeSpan(0, 17, 30, 0, 0), 4, false, 50, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 51, new TimeSpan(0, 20, 0, 0, 0), 4, false, 51, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 52, new TimeSpan(0, 23, 30, 0, 0), 4, false, 52, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 53, new TimeSpan(0, 11, 0, 0, 0), 2, false, 53, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 54, new TimeSpan(0, 14, 30, 0, 0), 2, false, 54, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 71, new TimeSpan(0, 15, 30, 0, 0), 2, false, 71, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 55, new TimeSpan(0, 17, 0, 0, 0), 2, false, 55, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 57, new TimeSpan(0, 9, 45, 0, 0), 5, false, 57, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 58, new TimeSpan(0, 12, 15, 0, 0), 5, false, 58, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 59, new TimeSpan(0, 15, 45, 0, 0), 5, false, 59, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 60, new TimeSpan(0, 18, 15, 0, 0), 5, false, 60, 2, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 61, new TimeSpan(0, 6, 45, 0, 0), 2, false, 61, 5, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 62, new TimeSpan(0, 9, 15, 0, 0), 2, false, 62, 5, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 63, new TimeSpan(0, 12, 45, 0, 0), 2, false, 63, 5, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 64, new TimeSpan(0, 15, 15, 0, 0), 2, false, 64, 5, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 65, new TimeSpan(0, 6, 30, 0, 0), 6, false, 65, 2, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 66, new TimeSpan(0, 9, 0, 0, 0), 6, false, 66, 2, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 67, new TimeSpan(0, 12, 30, 0, 0), 6, false, 67, 2, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 68, new TimeSpan(0, 15, 0, 0, 0), 6, false, 68, 2, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 69, new TimeSpan(0, 9, 30, 0, 0), 2, false, 69, 6, 3 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 56, new TimeSpan(0, 20, 30, 0, 0), 2, false, 56, 4, 1 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "IsCanceled", "Number", "OriginAirportId", "PlaneId" },
                values: new object[] { 41, new TimeSpan(0, 13, 40, 0, 0), 3, false, 41, 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 96);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 26,
                column: "DepartureTime",
                value: new TimeSpan(0, 20, 5, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 28,
                column: "DepartureTime",
                value: new TimeSpan(0, 2, 5, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 30,
                column: "DepartureTime",
                value: new TimeSpan(0, 17, 5, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 32,
                column: "DepartureTime",
                value: new TimeSpan(0, 23, 5, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 34,
                column: "DepartureTime",
                value: new TimeSpan(0, 14, 15, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 36,
                column: "DepartureTime",
                value: new TimeSpan(0, 20, 15, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 38,
                column: "DepartureTime",
                value: new TimeSpan(0, 17, 15, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 40,
                column: "DepartureTime",
                value: new TimeSpan(0, 23, 15, 0, 0));
        }
    }
}
