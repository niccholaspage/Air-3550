// 20210428000849_FixDTWtoDENFlightSeeding.cs - Air 3550 Project
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

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class FixDTWtoDENFlightSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 105,
                column: "DepartureTime",
                value: new TimeSpan(0, 14, 30, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 106,
                column: "DepartureTime",
                value: new TimeSpan(0, 17, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 107,
                column: "DepartureTime",
                value: new TimeSpan(0, 20, 30, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 108,
                column: "DepartureTime",
                value: new TimeSpan(0, 23, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 105,
                column: "DepartureTime",
                value: new TimeSpan(0, 16, 30, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 106,
                column: "DepartureTime",
                value: new TimeSpan(0, 19, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 107,
                column: "DepartureTime",
                value: new TimeSpan(0, 22, 30, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 108,
                column: "DepartureTime",
                value: new TimeSpan(1, 1, 0, 0, 0));
        }
    }
}
