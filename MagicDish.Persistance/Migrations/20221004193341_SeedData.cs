using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicDish.Persistance.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Vegetable" },
                    { 2, "Fruit" },
                    { 3, "Dairy" },
                    { 4, "Starch" },
                    { 5, "Fat" },
                    { 6, "Spices" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "UnitName" },
                values: new object[,]
                {
                    { 1, "Grams" },
                    { 2, "Mililiters" }
                });

            migrationBuilder.InsertData(
                table: "AvailableProducts",
                columns: new[] { "Id", "IsVegan", "Name", "ProductCategoryId", "UnitId" },
                values: new object[,]
                {
                    { 1, false, "Tomato", 1, 1 },
                    { 2, false, "Potato", 1, 1 },
                    { 3, false, "Carrot", 1, 1 },
                    { 4, false, "Apple", 2, 1 },
                    { 5, false, "Lemon", 2, 1 },
                    { 6, false, "Cheese", 3, 1 },
                    { 7, false, "Milk", 3, 2 },
                    { 8, false, "Flour", 4, 1 },
                    { 9, false, "Pasta", 4, 1 },
                    { 10, false, "Olive oil", 5, 1 },
                    { 11, false, "Butter", 5, 1 },
                    { 12, false, "Salt", 6, 1 },
                    { 13, false, "Pepper", 6, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AvailableProducts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
