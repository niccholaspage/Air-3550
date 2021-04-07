﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class SeedMoreAirports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 1,
                columns: new[] { "Elevation", "Latitude", "Longitude" },
                values: new object[] { 791, 41.411667m, -81.849722m });

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 2,
                columns: new[] { "Elevation", "Latitude", "Longitude" },
                values: new object[] { 599, 36.126667m, -86.681944m });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 3, "Detroit", "DTW", 645, 42.2125m, -83.353333m, "Michigan" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 4, "Atlanta", "ATL", 1026, 33.636667m, -84.428056m, "Georgia" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 5, "New York City", "JFK", 13, 40.639722m, -73.778889m, "New York" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 1,
                columns: new[] { "Elevation", "Latitude", "Longitude" },
                values: new object[] { 0, 0m, 0m });

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 2,
                columns: new[] { "Elevation", "Latitude", "Longitude" },
                values: new object[] { 0, 0m, 0m });
        }
    }
}