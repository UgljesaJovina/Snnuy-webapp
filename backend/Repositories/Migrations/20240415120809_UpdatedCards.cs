using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfLikes",
                table: "CustomCards");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "CustomCards",
                newName: "ApprovalState");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApprovalState",
                table: "CustomCards",
                newName: "State");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLikes",
                table: "CustomCards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
