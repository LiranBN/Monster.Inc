using Microsoft.EntityFrameworkCore.Migrations;

namespace Monster.Inc.Web.Data.Migrations
{
    public partial class Doors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Intimidiators_ApplicationUserId",
                table: "Intimidiators");

            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Energy = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intimidiators_ApplicationUserId",
                table: "Intimidiators",
                column: "ApplicationUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doors");

            migrationBuilder.DropIndex(
                name: "IX_Intimidiators_ApplicationUserId",
                table: "Intimidiators");

            migrationBuilder.CreateIndex(
                name: "IX_Intimidiators_ApplicationUserId",
                table: "Intimidiators",
                column: "ApplicationUserId");
        }
    }
}
