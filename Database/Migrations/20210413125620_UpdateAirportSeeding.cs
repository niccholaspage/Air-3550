using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class UpdateAirportSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 5,
                columns: new[] { "Code", "Elevation", "Latitude", "Longitude" },
                values: new object[] { "LGA", 21, 40.775m, -73.875m });

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 7,
                columns: new[] { "Code", "Latitude", "Longitude" },
                values: new object[] { "MDW", 41.786111m, -87.7525m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 5,
                columns: new[] { "Code", "Elevation", "Latitude", "Longitude" },
                values: new object[] { "JFK", 13, 40.639722m, -73.778889m });

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 7,
                columns: new[] { "Code", "Latitude", "Longitude" },
                values: new object[] { "ORD", 41.978611m, -87.904722m });
        }
    }
}
