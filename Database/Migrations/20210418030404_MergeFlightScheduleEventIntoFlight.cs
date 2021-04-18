using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class MergeFlightScheduleEventIntoFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightScheduleEvents");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FlightDepartureTime",
                table: "Flights",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "PlaneId",
                table: "Flights",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PlaneId",
                table: "Flights",
                column: "PlaneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Planes_PlaneId",
                table: "Flights",
                column: "PlaneId",
                principalTable: "Planes",
                principalColumn: "PlaneId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Planes_PlaneId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_PlaneId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FlightDepartureTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "PlaneId",
                table: "Flights");

            migrationBuilder.CreateTable(
                name: "FlightScheduleEvents",
                columns: table => new
                {
                    FlightScheduleEventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DefaultPlanePlaneId = table.Column<int>(type: "INTEGER", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FlightDepartureTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightScheduleEvents", x => x.FlightScheduleEventId);
                    table.ForeignKey(
                        name: "FK_FlightScheduleEvents_Planes_DefaultPlanePlaneId",
                        column: x => x.DefaultPlanePlaneId,
                        principalTable: "Planes",
                        principalColumn: "PlaneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightScheduleEvents_DefaultPlanePlaneId",
                table: "FlightScheduleEvents",
                column: "DefaultPlanePlaneId");
        }
    }
}
