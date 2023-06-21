using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.
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
    
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { 
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        base.OnConfiguring(optionsBuilder);

        string connectionString = "Server=localhost;Database=adventuretime;Uid=root;Pwd=password;";
        optionsBuilder.UseMySqlConnection(connectionString);
        optionsBuilder.UseMySQL(connectionString);
    }
}
