﻿// <auto-generated />
using System;
using ConsoleApp1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleApp1.Migrations
{
    [DbContext(typeof(GameDbContext))]
    partial class GameDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ConsoleApp1.Models.Ability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Stat")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Equipments.Amulet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Aspects")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DamageReduction")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("LevelRequirement")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("Stats")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Amulets");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Equipments.Armor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArmorType")
                        .HasColumnType("int");

                    b.Property<string>("Aspects")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DamageReduction")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("LevelRequirement")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("Stats")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Armors");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Equipments.Ring", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Aspects")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DamageReduction")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("LevelRequirement")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("Stats")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Rings");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Equipments.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Aspects")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DamageReduction")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("LevelRequirement")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("Stats")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("ConsoleApp1.Models.NPC", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AC")
                        .HasColumnType("int");

                    b.Property<int>("AttackValue")
                        .HasColumnType("int");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("NPCs");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AC")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EquippedAmuletId")
                        .HasColumnType("int");

                    b.Property<int>("EquippedBootsArmorId")
                        .HasColumnType("int");

                    b.Property<int>("EquippedChestArmorId")
                        .HasColumnType("int");

                    b.Property<int>("EquippedGlovesArmorId")
                        .HasColumnType("int");

                    b.Property<int>("EquippedHeadArmorId")
                        .HasColumnType("int");

                    b.Property<int>("EquippedPantsArmorId")
                        .HasColumnType("int");

                    b.Property<int>("EquippedRingId")
                        .HasColumnType("int");

                    b.Property<int>("EquippedWeaponId")
                        .HasColumnType("int");

                    b.Property<int>("Gold")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("XP")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquippedAmuletId");

                    b.HasIndex("EquippedBootsArmorId");

                    b.HasIndex("EquippedChestArmorId");

                    b.HasIndex("EquippedGlovesArmorId");

                    b.HasIndex("EquippedHeadArmorId");

                    b.HasIndex("EquippedPantsArmorId");

                    b.HasIndex("EquippedRingId");

                    b.HasIndex("EquippedWeaponId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("RewardXP")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerQuests");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Ability", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Player", null)
                        .WithMany("Abilities")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Equipments.Amulet", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Player", null)
                        .WithMany("Amulets")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Equipments.Armor", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Player", null)
                        .WithMany("Armors")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Equipments.Ring", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Player", null)
                        .WithMany("Rings")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Equipments.Weapon", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Player", null)
                        .WithMany("Weapons")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Game", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("ConsoleApp1.Models.NPC", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Game", null)
                        .WithMany("NPCs")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Player", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Equipments.Amulet", "EquippedAmulet")
                        .WithMany()
                        .HasForeignKey("EquippedAmuletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Equipments.Armor", "EquippedBootsArmor")
                        .WithMany()
                        .HasForeignKey("EquippedBootsArmorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Equipments.Armor", "EquippedChestArmor")
                        .WithMany()
                        .HasForeignKey("EquippedChestArmorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Equipments.Armor", "EquippedGlovesArmor")
                        .WithMany()
                        .HasForeignKey("EquippedGlovesArmorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Equipments.Armor", "EquippedHeadArmor")
                        .WithMany()
                        .HasForeignKey("EquippedHeadArmorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Equipments.Armor", "EquippedPantsArmor")
                        .WithMany()
                        .HasForeignKey("EquippedPantsArmorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Equipments.Ring", "EquippedRing")
                        .WithMany()
                        .HasForeignKey("EquippedRingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Models.Equipments.Weapon", "EquippedWeapon")
                        .WithMany()
                        .HasForeignKey("EquippedWeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquippedAmulet");

                    b.Navigation("EquippedBootsArmor");

                    b.Navigation("EquippedChestArmor");

                    b.Navigation("EquippedGlovesArmor");

                    b.Navigation("EquippedHeadArmor");

                    b.Navigation("EquippedPantsArmor");

                    b.Navigation("EquippedRing");

                    b.Navigation("EquippedWeapon");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Quest", b =>
                {
                    b.HasOne("ConsoleApp1.Models.Player", null)
                        .WithMany("Quests")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Game", b =>
                {
                    b.Navigation("NPCs");
                });

            modelBuilder.Entity("ConsoleApp1.Models.Player", b =>
                {
                    b.Navigation("Abilities");

                    b.Navigation("Amulets");

                    b.Navigation("Armors");

                    b.Navigation("Quests");

                    b.Navigation("Rings");

                    b.Navigation("Weapons");
                });
#pragma warning restore 612, 618
        }
    }
}
