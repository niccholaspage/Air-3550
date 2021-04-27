// 20210407224818_SeedPlanes.cs - Air 3550 Project
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
    public partial class SeedPlanes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxSeats", "Model" },
                values: new object[] { 1, 230, "Boeing 737 MAX" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxSeats", "Model" },
                values: new object[] { 2, 416, "Boeing 747" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxSeats", "Model" },
                values: new object[] { 4, 550, "Boeing 777" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 4);
        }
    }
}
