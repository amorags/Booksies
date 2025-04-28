using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookList.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Publishers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Books");
        }
    }
}
