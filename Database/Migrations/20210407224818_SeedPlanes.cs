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
