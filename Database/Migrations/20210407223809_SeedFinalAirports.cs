// 20210407223809_SeedFinalAirports.cs - Air 3550 Project
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
    public partial class SeedFinalAirports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 6, "Los Angeles", "LAX", 128, 33.9425m, -118.408056m, "California" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 7, "Chicago", "ORD", 668, 41.978611m, -87.904722m, "Illinois" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 8, "Dallas", "DFW", 607, 32.896944m, -97.038056m, "Ohio" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 9, "Denver", "DEN", 5434, 39.861667m, -104.673056m, "Colorado" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 10, "Seattle", "SEA", 433, 47.448889m, -122.309444m, "Washington" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 10);
        }
    }
}
