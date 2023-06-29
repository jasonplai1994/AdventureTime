using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    /// <inheritdoc />
    public partial class StoreQuests3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerQuests_Players_PlayerId",
                table: "PlayerQuests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerQuests",
                table: "PlayerQuests");

            migrationBuilder.RenameTable(
                name: "PlayerQuests",
                newName: "GameQuests");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerQuests_PlayerId",
                table: "GameQuests",
                newName: "IX_GameQuests_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameQuests",
                table: "GameQuests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameQuests_Players_PlayerId",
                table: "GameQuests",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameQuests_Players_PlayerId",
                table: "GameQuests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameQuests",
                table: "GameQuests");

            migrationBuilder.RenameTable(
                name: "GameQuests",
                newName: "PlayerQuests");

            migrationBuilder.RenameIndex(
                name: "IX_GameQuests_PlayerId",
                table: "PlayerQuests",
                newName: "IX_PlayerQuests_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerQuests",
                table: "PlayerQuests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerQuests_Players_PlayerId",
                table: "PlayerQuests",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
