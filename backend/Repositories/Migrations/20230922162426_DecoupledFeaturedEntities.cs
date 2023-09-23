using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class DecoupledFeaturedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomCards_UserAccounts_CardSetterId",
                table: "CustomCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_UserAccounts_DeckSetterId",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_Decks_DeckSetterId",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_CustomCards_CardSetterId",
                table: "CustomCards");

            migrationBuilder.DropColumn(
                name: "DeckSetterId",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "SettingDate",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "CardSetterId",
                table: "CustomCards");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CustomCards");

            migrationBuilder.DropColumn(
                name: "SettingDate",
                table: "CustomCards");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLikes",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLikes",
                table: "CustomCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomCardsOTD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SettingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardSetterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCardsOTD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomCardsOTD_CustomCards_CardId",
                        column: x => x.CardId,
                        principalTable: "CustomCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomCardsOTD_UserAccounts_CardSetterId",
                        column: x => x.CardSetterId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DecksOTD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SettingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeckSetterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecksOTD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecksOTD_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DecksOTD_UserAccounts_DeckSetterId",
                        column: x => x.DeckSetterId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomCardsOTD_CardId",
                table: "CustomCardsOTD",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCardsOTD_CardSetterId",
                table: "CustomCardsOTD",
                column: "CardSetterId");

            migrationBuilder.CreateIndex(
                name: "IX_DecksOTD_DeckId",
                table: "DecksOTD",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_DecksOTD_DeckSetterId",
                table: "DecksOTD",
                column: "DeckSetterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomCardsOTD");

            migrationBuilder.DropTable(
                name: "DecksOTD");

            migrationBuilder.DropColumn(
                name: "NumberOfLikes",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "NumberOfLikes",
                table: "CustomCards");

            migrationBuilder.AddColumn<Guid>(
                name: "DeckSetterId",
                table: "Decks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Decks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SettingDate",
                table: "Decks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CardSetterId",
                table: "CustomCards",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "CustomCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SettingDate",
                table: "CustomCards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Decks_DeckSetterId",
                table: "Decks",
                column: "DeckSetterId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCards_CardSetterId",
                table: "CustomCards",
                column: "CardSetterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCards_UserAccounts_CardSetterId",
                table: "CustomCards",
                column: "CardSetterId",
                principalTable: "UserAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_UserAccounts_DeckSetterId",
                table: "Decks",
                column: "DeckSetterId",
                principalTable: "UserAccounts",
                principalColumn: "Id");
        }
    }
}
