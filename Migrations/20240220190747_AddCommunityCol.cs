using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quibby.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunityCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdultContent",
                table: "Communities",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Communities",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdultContent",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Communities");
        }
    }
}
