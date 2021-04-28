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
