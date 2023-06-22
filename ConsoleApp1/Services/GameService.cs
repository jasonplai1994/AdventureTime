using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ErrorOr<Created> AddNPC(List<NPC> NPCs)
        {
            int i = 0;

            while (i < NPCs.Count)
            {
                _dbContext.Add(NPCs[i]);
                i++;
            }
            _dbContext.SaveChanges ();
            return Result.Created;
        }

        public ErrorOr<Created> SaveQuests(List<Quest> quests)
        {
            int i = 0;

            while (i < quests.Count)
            {
                _dbContext.Add(quests[i]);
                i++;
            }
            _dbContext.SaveChanges();
            return Result.Created;
        }
    }
}
