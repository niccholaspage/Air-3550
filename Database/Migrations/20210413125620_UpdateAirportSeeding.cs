// 20210413125620_UpdateAirportSeeding.cs - Air 3550 Project
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

namespace Database.Migrations
{
    public partial class UpdateAirportSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 5,
                columns: new[] { "Code", "Elevation", "Latitude", "Longitude" },
                values: new object[] { "LGA", 21, 40.775m, -73.875m });

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 7,
                columns: new[] { "Code", "Latitude", "Longitude" },
                values: new object[] { "MDW", 41.786111m, -87.7525m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 5,
                columns: new[] { "Code", "Elevation", "Latitude", "Longitude" },
                values: new object[] { "JFK", 13, 40.639722m, -73.778889m });

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 7,
                columns: new[] { "Code", "Latitude", "Longitude" },
                values: new object[] { "ORD", 41.978611m, -87.904722m });
        }
    }
}
