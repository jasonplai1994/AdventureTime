using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    /// <inheritdoc />
    public partial class Equipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentInventory");

            migrationBuilder.DropColumn(
                name: "CurrentDayNumber",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TimePeriod",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TurnNumber",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Weather",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Weapons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Rings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquippedAmuletId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquippedBootsArmorId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquippedChestArmorId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquippedGlovesArmorId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquippedHeadArmorId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquippedPantsArmorId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquippedRingId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquippedWeaponId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmorType",
                table: "Armors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Amulets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquippedAmuletId",
                table: "Players",
                column: "EquippedAmuletId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquippedBootsArmorId",
                table: "Players",
                column: "EquippedBootsArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquippedChestArmorId",
                table: "Players",
                column: "EquippedChestArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquippedGlovesArmorId",
                table: "Players",
                column: "EquippedGlovesArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquippedHeadArmorId",
                table: "Players",
                column: "EquippedHeadArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquippedPantsArmorId",
                table: "Players",
                column: "EquippedPantsArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquippedRingId",
                table: "Players",
                column: "EquippedRingId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquippedWeaponId",
                table: "Players",
                column: "EquippedWeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Amulets_EquippedAmuletId",
                table: "Players",
                column: "EquippedAmuletId",
                principalTable: "Amulets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Armors_EquippedBootsArmorId",
                table: "Players",
                column: "EquippedBootsArmorId",
                principalTable: "Armors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Armors_EquippedChestArmorId",
                table: "Players",
                column: "EquippedChestArmorId",
                principalTable: "Armors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Armors_EquippedGlovesArmorId",
                table: "Players",
                column: "EquippedGlovesArmorId",
                principalTable: "Armors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Armors_EquippedHeadArmorId",
                table: "Players",
                column: "EquippedHeadArmorId",
                principalTable: "Armors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Armors_EquippedPantsArmorId",
                table: "Players",
                column: "EquippedPantsArmorId",
                principalTable: "Armors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Rings_EquippedRingId",
                table: "Players",
                column: "EquippedRingId",
                principalTable: "Rings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Weapons_EquippedWeaponId",
                table: "Players",
                column: "EquippedWeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Amulets_EquippedAmuletId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Armors_EquippedBootsArmorId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Armors_EquippedChestArmorId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Armors_EquippedGlovesArmorId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Armors_EquippedHeadArmorId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Armors_EquippedPantsArmorId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Rings_EquippedRingId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Weapons_EquippedWeaponId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquippedAmuletId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquippedBootsArmorId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquippedChestArmorId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquippedGlovesArmorId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquippedHeadArmorId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquippedPantsArmorId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquippedRingId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquippedWeaponId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Rings");

            migrationBuilder.DropColumn(
                name: "EquippedAmuletId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EquippedBootsArmorId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EquippedChestArmorId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EquippedGlovesArmorId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EquippedHeadArmorId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EquippedPantsArmorId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EquippedRingId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EquippedWeaponId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ArmorType",
                table: "Armors");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Amulets");

            migrationBuilder.AddColumn<int>(
                name: "CurrentDayNumber",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimePeriod",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TurnNumber",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Weather",
                table: "Games",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EquipmentInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsConsumable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsEquipped = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LegendarySkill = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentInventory_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentInventory_PlayerId",
                table: "EquipmentInventory",
                column: "PlayerId");
        }
    }
}
