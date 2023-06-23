using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1;

public class GameDbContext : DbContext 
{
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Quest> Quests { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<NPC> NPCs { get; set; } = null!;
    public DbSet<Equipment> Equipments { get; set; } = null!;

    /*public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { 
        
    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=localhost;Database=adventuretime;user=root;password=Monkey2000!;";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}