using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Models;
using Pomelo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using ConsoleApp1.Models.Equipments;

namespace ConsoleApp1;

public class GameDbContext : DbContext
{
    public DbSet<Game> Games { get; set; } = null!;
    /*public DbSet<Quest> GameQuests { get; set; } = null!;*/
    public DbSet<Quest> PlayerQuests { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<NPC> NPCs { get; set; } = null!;
/*    public DbSet<Equipment> EquipmentInventory { get; set; } = null!;
*/    /*public DbSet<Equipment> EquipmentStore { get; set; } = null!;*/
    public DbSet<Armor> Armors { get; set; } = null!;
    public DbSet<Amulet> Amulets { get; set; } = null!;
    public DbSet<Ring> Rings { get; set; } = null!;
    public DbSet<Weapon> Weapons { get; set; } = null!;
    public DbSet<Ability> Abilities { get; set; } = null!;

    /*public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { 
        
    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //string connectionString = "Server=localhost;Database=adventuretime;user=root;password=password;";


        string connectionString = Environment.GetEnvironmentVariable("localhost");
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.Entity<Equipment>()
            .ToTable("EquipmentInventory") // Configure EquipmentInventory table
            .HasKey(e => e.Id);*/
/*
        modelBuilder.Entity<Equipment>()
            .ToTable("EquipmentStore") // Configure EquipmentStore table
            .HasKey(e => e.Id);

        modelBuilder.Entity<Quest>()
            .ToTable("GameQuests") // Configure EquipmentInventory table
            .HasKey(e => e.Id);*/

        /*modelBuilder.Entity<Quest>()
            .ToTable("PlayerQuests") // Configure EquipmentStore table
            .HasKey(e => e.Id);*/

    }
}