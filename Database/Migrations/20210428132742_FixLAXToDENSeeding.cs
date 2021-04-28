using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class FixLAXToDENSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 186,
                column: "DepartureTime",
                value: new TimeSpan(0, 12, 35, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 187,
                column: "DepartureTime",
                value: new TimeSpan(0, 15, 5, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 188,
                column: "DepartureTime",
                value: new TimeSpan(0, 18, 35, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 186,
                column: "DepartureTime",
                value: new TimeSpan(0, 9, 35, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 187,
                column: "DepartureTime",
                value: new TimeSpan(0, 9, 5, 0, 0));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 188,
                column: "DepartureTime",
                value: new TimeSpan(0, 9, 35, 0, 0));
        }
    }
}
