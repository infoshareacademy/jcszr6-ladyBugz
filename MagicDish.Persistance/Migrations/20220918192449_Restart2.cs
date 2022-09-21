using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicDish.Persistance.Migrations
{
    public partial class Restart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_AspNetUsers_UserId1",
                table: "Fridges");

            migrationBuilder.DropIndex(
                name: "IX_Fridges_UserId1",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Fridges");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Fridges",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_UserId1",
                table: "Fridges",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_AspNetUsers_UserId1",
                table: "Fridges",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}