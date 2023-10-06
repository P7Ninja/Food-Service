using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodService.Migrations
{
    /// <inheritdoc />
    public partial class Ini2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InventoryItems");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_FoodId",
                table: "InventoryItems",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Foods_FoodId",
                table: "InventoryItems",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Foods_FoodId",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_FoodId",
                table: "InventoryItems");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "InventoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
