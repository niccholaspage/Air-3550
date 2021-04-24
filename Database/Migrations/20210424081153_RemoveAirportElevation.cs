using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class RemoveAirportElevation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Elevation",
                table: "Airports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Elevation",
                table: "Airports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 1,
                column: "Elevation",
                value: 791);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 2,
                column: "Elevation",
                value: 599);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 3,
                column: "Elevation",
                value: 645);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 4,
                column: "Elevation",
                value: 1026);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 5,
                column: "Elevation",
                value: 21);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 6,
                column: "Elevation",
                value: 128);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 7,
                column: "Elevation",
                value: 668);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 8,
                column: "Elevation",
                value: 607);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 9,
                column: "Elevation",
                value: 5434);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 10,
                column: "Elevation",
                value: 433);
        }
    }
}
