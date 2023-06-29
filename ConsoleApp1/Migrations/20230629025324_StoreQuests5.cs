using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    /// <inheritdoc />
    public partial class StoreQuests5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentStore_Players_PlayerId",
                table: "EquipmentStore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentStore",
                table: "EquipmentStore");

            migrationBuilder.RenameTable(
                name: "EquipmentStore",
                newName: "EquipmentInventory");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentStore_PlayerId",
                table: "EquipmentInventory",
                newName: "IX_EquipmentInventory_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentInventory",
                table: "EquipmentInventory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentInventory_Players_PlayerId",
                table: "EquipmentInventory",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentInventory_Players_PlayerId",
                table: "EquipmentInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentInventory",
                table: "EquipmentInventory");

            migrationBuilder.RenameTable(
                name: "EquipmentInventory",
                newName: "EquipmentStore");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentInventory_PlayerId",
                table: "EquipmentStore",
                newName: "IX_EquipmentStore_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentStore",
                table: "EquipmentStore",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentStore_Players_PlayerId",
                table: "EquipmentStore",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
