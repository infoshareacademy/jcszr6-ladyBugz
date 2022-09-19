using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicDish.Persistance.Migrations
{
    public partial class Restart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Fridges_FridgeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FridgeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FridgeId",
                table: "AspNetUsers");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "FridgeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FridgeId",
                table: "AspNetUsers",
                column: "FridgeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Fridges_FridgeId",
                table: "AspNetUsers",
                column: "FridgeId",
                principalTable: "Fridges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}