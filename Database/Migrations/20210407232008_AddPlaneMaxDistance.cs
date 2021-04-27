// 20210407232008_AddPlaneMaxDistance.cs - Air 3550 Project
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
    public partial class AddPlaneMaxDistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxDistance",
                table: "Planes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 1,
                column: "MaxDistance",
                value: 6570);

            migrationBuilder.UpdateData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 2,
                column: "MaxDistance",
                value: 14815);

            migrationBuilder.UpdateData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 4,
                column: "MaxDistance",
                value: 17395);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDistance",
                table: "Planes");
        }
    }
}
