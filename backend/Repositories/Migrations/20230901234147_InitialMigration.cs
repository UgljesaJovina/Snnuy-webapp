using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ManaCost = table.Column<int>(type: "int", nullable: false),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    HealthValue = table.Column<int>(type: "int", nullable: false),
                    CardImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Regions = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardCode);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Permissions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EffectText = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ManaCost = table.Column<int>(type: "int", nullable: false),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    HealthValue = table.Column<int>(type: "int", nullable: false),
                    CardDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CardImageName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    OwnerAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomCards_UserAccounts_OwnerAccountId",
                        column: x => x.OwnerAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    DeckCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeckName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eternal = table.Column<bool>(type: "bit", nullable: false),
                    OwnerAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.DeckCode);
                    table.ForeignKey(
                        name: "FK_Decks_UserAccounts_OwnerAccountId",
                        column: x => x.OwnerAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CustomCards_OwnerAccountId",
                table: "CustomCards",
                column: "OwnerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckItem_CardCode",
                table: "DeckItem",
                column: "CardCode");

            migrationBuilder.CreateIndex(
                name: "IX_DeckItem_DeckCode",
                table: "DeckItem",
                column: "DeckCode");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_OwnerAccountId",
                table: "Decks",
                column: "OwnerAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomCards");

            migrationBuilder.DropTable(
                name: "DeckItem");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
