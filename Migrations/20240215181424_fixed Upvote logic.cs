using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quibby.Migrations
{
    /// <inheritdoc />
    public partial class fixedUpvotelogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpvote",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Isdownvote",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "Answers");

            migrationBuilder.AddColumn<long>(
                name: "Downvote",
                table: "Votes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Upvote",
                table: "Votes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvote",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Upvote",
                table: "Votes");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpvote",
                table: "Votes",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Isdownvote",
                table: "Votes",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
