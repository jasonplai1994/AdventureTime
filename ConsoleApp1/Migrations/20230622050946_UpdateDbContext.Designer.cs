﻿// <auto-generated />
using System;
using ConsoleApp1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleApp1.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20230622050946_UpdateDbContext")]
    partial class UpdateDbContext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ConsoleApp1.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LegendarySkill")
                        .HasColumnType("longtext");

                    b.Property<int?>("NPCId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NPCId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("ConsoleApp1.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CurrentDayNumber")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("TimePeriod")
                        .HasColumnType("int");

                    b.Property<int>("TurnNumber")
                        .HasColumnType("int");

                    b.Property<string>("Weather")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("ConsoleApp1.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ConsoleApp1.NPC", b =>
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

            modelBuilder.Entity("ConsoleApp1.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AC")
                        .HasColumnType("int");

                    b.Property<int>("AmuletId")
                        .HasColumnType("int");

                    b.Property<int>("ArmorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gold")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Quest")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RingId")
                        .HasColumnType("int");

                    b.Property<int>("UseableId")
                        .HasColumnType("int");

                    b.Property<int>("WeaponId")
                        .HasColumnType("int");

                    b.Property<int>("XP")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AmuletId");

                    b.HasIndex("ArmorId");

                    b.HasIndex("RingId");

                    b.HasIndex("UseableId");

                    b.HasIndex("WeaponId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ConsoleApp1.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("NPCId")
                        .HasColumnType("int");

                    b.Property<int>("RewardXP")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("NPCId");

                    b.ToTable("Quests");
                });

            modelBuilder.Entity("ConsoleApp1.Equipment", b =>
                {
                    b.HasOne("ConsoleApp1.NPC", null)
                        .WithMany("Items")
                        .HasForeignKey("NPCId");

                    b.HasOne("ConsoleApp1.Player", null)
                        .WithMany("Inventory")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("ConsoleApp1.Game", b =>
                {
                    b.HasOne("ConsoleApp1.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("ConsoleApp1.NPC", b =>
                {
                    b.HasOne("ConsoleApp1.Game", null)
                        .WithMany("NPCs")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("ConsoleApp1.Player", b =>
                {
                    b.HasOne("ConsoleApp1.Equipment", "Amulet")
                        .WithMany()
                        .HasForeignKey("AmuletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Equipment", "Armor")
                        .WithMany()
                        .HasForeignKey("ArmorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Equipment", "Ring")
                        .WithMany()
                        .HasForeignKey("RingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Equipment", "Useable")
                        .WithMany()
                        .HasForeignKey("UseableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp1.Equipment", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Amulet");

                    b.Navigation("Armor");

                    b.Navigation("Ring");

                    b.Navigation("Useable");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("ConsoleApp1.Quest", b =>
                {
                    b.HasOne("ConsoleApp1.NPC", null)
                        .WithMany("Quests")
                        .HasForeignKey("NPCId");
                });

            modelBuilder.Entity("ConsoleApp1.Game", b =>
                {
                    b.Navigation("NPCs");
                });

            modelBuilder.Entity("ConsoleApp1.NPC", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Quests");
                });

            modelBuilder.Entity("ConsoleApp1.Player", b =>
                {
                    b.Navigation("Inventory");
                });
#pragma warning restore 612, 618
        }
    }
}