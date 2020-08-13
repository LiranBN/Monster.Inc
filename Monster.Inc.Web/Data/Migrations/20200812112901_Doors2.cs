using Microsoft.EntityFrameworkCore.Migrations;

namespace Monster.Inc.Web.Data.Migrations
{
    public partial class Doors2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doors",
                columns: new[] { "Id", "Energy", "Name" },
                values: new object[,]
                {
                    { 1, 90.0, "Yellow" },
                    { 2, 100.0, "Red" },
                    { 3, 110.0, "Pink" },
                    { 4, 220.0, "Black" },
                    { 5, 240.0, "Green" },
                    { 6, 140.0, "Blue" },
                    { 7, 160.0, "Putple" },
                    { 8, 160.0, "Grey" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Doors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Doors",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
