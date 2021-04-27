// 20210410150835_FixSeeding.cs - Air 3550 Project
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
    public partial class FixSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 8,
                column: "State",
                value: "Texas");

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxDistance", "MaxSeats", "Model" },
                values: new object[] { 3, 17395, 550, "Boeing 777" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 8,
                column: "State",
                value: "Ohio");

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxDistance", "MaxSeats", "Model" },
                values: new object[] { 4, 17395, 550, "Boeing 777" });
        }
    }
}
