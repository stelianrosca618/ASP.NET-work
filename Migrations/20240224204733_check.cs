using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quibby.Migrations
{
    /// <inheritdoc />
    public partial class check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DownVote",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpVote",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DownVote",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpVote",
                table: "Answers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVote",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UpVote",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DownVote",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "UpVote",
                table: "Answers");
        }
    }
}
