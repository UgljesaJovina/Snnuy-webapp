using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class someMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DecksOTD_Decks_DeckCode",
                table: "DecksOTD");

            migrationBuilder.DropForeignKey(
                name: "FK_DecksOTD_UserAccounts_DeckSetterId",
                table: "DecksOTD");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckUserAccount_Decks_LikedDecksDeckCode",
                table: "DeckUserAccount");

            migrationBuilder.DropTable(
                name: "DeckItem");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeckUserAccount",
                table: "DeckUserAccount");

            migrationBuilder.DropIndex(
                name: "IX_DecksOTD_DeckCode",
                table: "DecksOTD");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Decks",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "LikedDecksDeckCode",
                table: "DeckUserAccount");

            migrationBuilder.DropColumn(
                name: "DeckCode",
                table: "DecksOTD");

            migrationBuilder.DropColumn(
                name: "UpVotes",
                table: "Decks");

            migrationBuilder.AddColumn<Guid>(
                name: "LikedDecksId",
                table: "DeckUserAccount",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DeckSetterId",
                table: "DecksOTD",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DeckId",
                table: "DecksOTD",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "DeckCode",
                table: "Decks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Decks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeckUserAccount",
                table: "DeckUserAccount",
                columns: new[] { "LikedDecksId", "LikedUsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Decks",
                table: "Decks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DecksOTD_DeckId",
                table: "DecksOTD",
                column: "DeckId");

            migrationBuilder.AddForeignKey(
                name: "FK_DecksOTD_Decks_DeckId",
                table: "DecksOTD",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DecksOTD_UserAccounts_DeckSetterId",
                table: "DecksOTD",
                column: "DeckSetterId",
                principalTable: "UserAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeckUserAccount_Decks_LikedDecksId",
                table: "DeckUserAccount",
                column: "LikedDecksId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DecksOTD_Decks_DeckId",
                table: "DecksOTD");

            migrationBuilder.DropForeignKey(
                name: "FK_DecksOTD_UserAccounts_DeckSetterId",
                table: "DecksOTD");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckUserAccount_Decks_LikedDecksId",
                table: "DeckUserAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeckUserAccount",
                table: "DeckUserAccount");

            migrationBuilder.DropIndex(
                name: "IX_DecksOTD_DeckId",
                table: "DecksOTD");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Decks",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "LikedDecksId",
                table: "DeckUserAccount");

            migrationBuilder.DropColumn(
                name: "DeckId",
                table: "DecksOTD");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Decks");

            migrationBuilder.AddColumn<string>(
                name: "LikedDecksDeckCode",
                table: "DeckUserAccount",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeckSetterId",
                table: "DecksOTD",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeckCode",
                table: "DecksOTD",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeckCode",
                table: "Decks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UpVotes",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeckUserAccount",
                table: "DeckUserAccount",
                columns: new[] { "LikedDecksDeckCode", "LikedUsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Decks",
                table: "Decks",
                column: "DeckCode");

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    CardImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HealthValue = table.Column<int>(type: "int", nullable: false),
                    ManaCost = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Regions = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardCode);
                });

            migrationBuilder.CreateTable(
                name: "DeckItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardCode = table.Column<string>(type: "nvarchar(7)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    DeckCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeckItem_Cards_CardCode",
                        column: x => x.CardCode,
                        principalTable: "Cards",
                        principalColumn: "CardCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckItem_Decks_DeckCode",
                        column: x => x.DeckCode,
                        principalTable: "Decks",
                        principalColumn: "DeckCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DecksOTD_DeckCode",
                table: "DecksOTD",
                column: "DeckCode");

            migrationBuilder.CreateIndex(
                name: "IX_DeckItem_CardCode",
                table: "DeckItem",
                column: "CardCode");

            migrationBuilder.CreateIndex(
                name: "IX_DeckItem_DeckCode",
                table: "DeckItem",
                column: "DeckCode");

            migrationBuilder.AddForeignKey(
                name: "FK_DecksOTD_Decks_DeckCode",
                table: "DecksOTD",
                column: "DeckCode",
                principalTable: "Decks",
                principalColumn: "DeckCode");

            migrationBuilder.AddForeignKey(
                name: "FK_DecksOTD_UserAccounts_DeckSetterId",
                table: "DecksOTD",
                column: "DeckSetterId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeckUserAccount_Decks_LikedDecksDeckCode",
                table: "DeckUserAccount",
                column: "LikedDecksDeckCode",
                principalTable: "Decks",
                principalColumn: "DeckCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
