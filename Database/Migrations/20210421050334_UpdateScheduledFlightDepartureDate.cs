using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class UpdateScheduledFlightDepartureDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureTimestamp",
                table: "ScheduledFlights",
                newName: "DepartureDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "ScheduledFlights",
                newName: "DepartureTimestamp");
        }
    }
}
