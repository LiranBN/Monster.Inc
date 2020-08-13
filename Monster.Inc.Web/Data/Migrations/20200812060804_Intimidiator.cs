using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Monster.Inc.Web.Data.Migrations
{
    public partial class Intimidiator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Intimidiators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    LastName = table.Column<string>(maxLength: 256, nullable: false),
                    TentaclesNumber = table.Column<string>(nullable: false),
                    StartedDate = table.Column<DateTime>(nullable: false),
                    ApplicationUserId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intimidiators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intimidiators_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intimidiators_ApplicationUserId",
                table: "Intimidiators",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intimidiators");
        }
    }
}
