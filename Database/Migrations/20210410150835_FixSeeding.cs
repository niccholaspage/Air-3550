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
