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
    public DbSet<Post> Posts { get; set; } = null!;
    
    /*public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { 
        
    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
        base.OnConfiguring(optionsBuilder);*/

        string connectionString = "Server=localhost;Database=adventuretime;user=root;password=Monkey2000!;";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<NPC> NPCs { get; set; }
}