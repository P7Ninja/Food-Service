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
            migrationBuilder.AddColumn<string>(
                name: "SearchName",
                table: "Foods",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchName",
                table: "Foods");
        }
    }
}
