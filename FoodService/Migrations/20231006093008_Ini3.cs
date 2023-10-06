using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodService.Migrations
{
    /// <inheritdoc />
    public partial class Ini3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MacroID",
                table: "Foods");

            migrationBuilder.AddColumn<float>(
                name: "Carbs",
                table: "Foods",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Fat",
                table: "Foods",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Protein",
                table: "Foods",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carbs",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Fat",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "Foods");

            migrationBuilder.AddColumn<int>(
                name: "MacroID",
                table: "Foods",
                type: "int",
                nullable: true);
        }
    }
}
