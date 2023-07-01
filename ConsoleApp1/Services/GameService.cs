using ConsoleApp1.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.Services
{
    public class GameService : IGameService
    {
        private readonly GameDbContext _dbContext;
        public GameService(GameDbContext dbContext) {

            _dbContext = dbContext;
        }

        public ErrorOr<Created> CreateGame(Game game) {
            _dbContext.Add(game);
            _dbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Created> UpdatePlayer(Player player)
        {
            if (player.Description.Equals("NEW PLAYER"))
                return Result.Created;

            _dbContext.Update(player);
            _dbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Created> SaveNewPlayer(Player player)
        {
            _dbContext.Add(player);
            _dbContext.SaveChanges();

            return Result.Created;
        }

        public Player GetPlayer(String name)
        {
            List<Player> x = _dbContext.Players
                .Include(p => p.Quests)
                .Include(p => p.Weapons)
                .Include(p => p.Armors)
                .Include(p => p.Amulets)
                .Include(p => p.Rings)
                .Include(p => p.Abilities)
                .ToList();

            foreach (Player p in x)
                if (p.Description.Equals(name))
                    return p;
            
            return null;
        }

        public List<Player> GetPlayerList()
        {
            return _dbContext.Players.ToList();
        }

        public ErrorOr<Deleted> DropTable(String tableName)
        {
            String sql = $"DROP TABLE IF EXISTS {tableName}";
            _dbContext.Database.ExecuteSqlRaw(sql);
            
            return Result.Deleted;
        }
    }
}
