using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    OwnerAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NumberOfLikes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomCards_UserAccounts_OwnerAccountId",
                        column: x => x.OwnerAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeckCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeckName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    NumberOfLikes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decks_UserAccounts_OwnerAccountId",
                        column: x => x.OwnerAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id");
                });

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
                name: "CustomCardUserAccount",
                columns: table => new
                {
                    LikedCustomCardsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LikedUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCardUserAccount", x => new { x.LikedCustomCardsId, x.LikedUsersId });
                    table.ForeignKey(
                        name: "FK_CustomCardUserAccount_CustomCards_LikedCustomCardsId",
                        column: x => x.LikedCustomCardsId,
                        principalTable: "CustomCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomCardUserAccount_UserAccounts_LikedUsersId",
                        column: x => x.LikedUsersId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "DeckUserAccount",
                columns: table => new
                {
                    LikedDecksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LikedUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckUserAccount", x => new { x.LikedDecksId, x.LikedUsersId });
                    table.ForeignKey(
                        name: "FK_DeckUserAccount_Decks_LikedDecksId",
                        column: x => x.LikedDecksId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckUserAccount_UserAccounts_LikedUsersId",
                        column: x => x.LikedUsersId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomCards_OwnerAccountId",
                table: "CustomCards",
                column: "OwnerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCardsOTD_CardId",
                table: "CustomCardsOTD",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCardsOTD_CardSetterId",
                table: "CustomCardsOTD",
                column: "CardSetterId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCardUserAccount_LikedUsersId",
                table: "CustomCardUserAccount",
                column: "LikedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_OwnerAccountId",
                table: "Decks",
                column: "OwnerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DecksOTD_DeckId",
                table: "DecksOTD",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_DecksOTD_DeckSetterId",
                table: "DecksOTD",
                column: "DeckSetterId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckUserAccount_LikedUsersId",
                table: "DeckUserAccount",
                column: "LikedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_Username",
                table: "UserAccounts",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomCardsOTD");

            migrationBuilder.DropTable(
                name: "CustomCardUserAccount");

            migrationBuilder.DropTable(
                name: "DecksOTD");

            migrationBuilder.DropTable(
                name: "DeckUserAccount");

            migrationBuilder.DropTable(
                name: "CustomCards");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
