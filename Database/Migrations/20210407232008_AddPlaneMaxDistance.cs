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
