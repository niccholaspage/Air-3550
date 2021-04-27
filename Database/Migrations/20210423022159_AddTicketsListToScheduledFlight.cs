// 20210423022159_AddTicketsListToScheduledFlight.cs - Air 3550 Project
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
    public partial class AddTicketsListToScheduledFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ScheduledFlights_ScheduledFlightId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduledFlightId",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ScheduledFlights_ScheduledFlightId",
                table: "Tickets",
                column: "ScheduledFlightId",
                principalTable: "ScheduledFlights",
                principalColumn: "ScheduledFlightId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ScheduledFlights_ScheduledFlightId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduledFlightId",
                table: "Tickets",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ScheduledFlights_ScheduledFlightId",
                table: "Tickets",
                column: "ScheduledFlightId",
                principalTable: "ScheduledFlights",
                principalColumn: "ScheduledFlightId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
