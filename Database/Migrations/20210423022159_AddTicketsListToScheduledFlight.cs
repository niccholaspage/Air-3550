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
