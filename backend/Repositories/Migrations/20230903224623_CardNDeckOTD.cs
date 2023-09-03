using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class CardNDeckOTD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomCards_UserAccounts_OwnerAccountId",
                table: "CustomCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_UserAccounts_OwnerAccountId",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "AttackPower",
                table: "CustomCards");

            migrationBuilder.DropColumn(
                name: "CardImageName",
                table: "CustomCards");

            migrationBuilder.DropColumn(
                name: "EffectText",
                table: "CustomCards");

            migrationBuilder.DropColumn(
                name: "HealthValue",
                table: "CustomCards");

            migrationBuilder.DropColumn(
                name: "ManaCost",
                table: "CustomCards");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "UserAccounts",
                newName: "Username");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerAccountId",
                table: "Decks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "UpVotes",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerAccountId",
                table: "CustomCards",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostingDate",
                table: "CustomCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CustomCardsOTD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SettingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SetAutomatically = table.Column<bool>(type: "bit", nullable: false),
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
                    DeckCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SettingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SetAutomatically = table.Column<bool>(type: "bit", nullable: false),
                    DeckSetterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecksOTD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecksOTD_Decks_DeckCode",
                        column: x => x.DeckCode,
                        principalTable: "Decks",
                        principalColumn: "DeckCode");
                    table.ForeignKey(
                        name: "FK_DecksOTD_UserAccounts_DeckSetterId",
                        column: x => x.DeckSetterId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeckUserAccount",
                columns: table => new
                {
                    LikedDecksDeckCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikedUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckUserAccount", x => new { x.LikedDecksDeckCode, x.LikedUsersId });
                    table.ForeignKey(
                        name: "FK_DeckUserAccount_Decks_LikedDecksDeckCode",
                        column: x => x.LikedDecksDeckCode,
                        principalTable: "Decks",
                        principalColumn: "DeckCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckUserAccount_UserAccounts_LikedUsersId",
                        column: x => x.LikedUsersId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CustomCardUserAccount_LikedUsersId",
                table: "CustomCardUserAccount",
                column: "LikedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_DecksOTD_DeckCode",
                table: "DecksOTD",
                column: "DeckCode");

            migrationBuilder.CreateIndex(
                name: "IX_DecksOTD_DeckSetterId",
                table: "DecksOTD",
                column: "DeckSetterId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckUserAccount_LikedUsersId",
                table: "DeckUserAccount",
                column: "LikedUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCards_UserAccounts_OwnerAccountId",
                table: "CustomCards",
                column: "OwnerAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_UserAccounts_OwnerAccountId",
                table: "Decks",
                column: "OwnerAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomCards_UserAccounts_OwnerAccountId",
                table: "CustomCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_UserAccounts_OwnerAccountId",
                table: "Decks");

            migrationBuilder.DropTable(
                name: "CustomCardsOTD");

            migrationBuilder.DropTable(
                name: "CustomCardUserAccount");

            migrationBuilder.DropTable(
                name: "DecksOTD");

            migrationBuilder.DropTable(
                name: "DeckUserAccount");

            migrationBuilder.DropColumn(
                name: "UpVotes",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "PostingDate",
                table: "CustomCards");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "UserAccounts",
                newName: "UserName");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerAccountId",
                table: "Decks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerAccountId",
                table: "CustomCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttackPower",
                table: "CustomCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CardImageName",
                table: "CustomCards",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EffectText",
                table: "CustomCards",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HealthValue",
                table: "CustomCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManaCost",
                table: "CustomCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCards_UserAccounts_OwnerAccountId",
                table: "CustomCards",
                column: "OwnerAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_UserAccounts_OwnerAccountId",
                table: "Decks",
                column: "OwnerAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
