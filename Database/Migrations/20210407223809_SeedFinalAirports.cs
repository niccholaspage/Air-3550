using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class SeedFinalAirports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 6, "Los Angeles", "LAX", 128, 33.9425m, -118.408056m, "California" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 7, "Chicago", "ORD", 668, 41.978611m, -87.904722m, "Illinois" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 8, "Dallas", "DFW", 607, 32.896944m, -97.038056m, "Ohio" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 9, "Denver", "DEN", 5434, 39.861667m, -104.673056m, "Colorado" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "City", "Code", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 10, "Seattle", "SEA", 433, 47.448889m, -122.309444m, "Washington" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 10);
        }
    }
}
