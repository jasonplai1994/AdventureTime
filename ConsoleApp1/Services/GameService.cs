using ConsoleApp1.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public ErrorOr<Created> AddNPC(List<NPC> NPCs)
        {
            int i = 0;

            while (i < NPCs.Count)
            {
                _dbContext.Add(NPCs[i]);
                i++;
            }
            _dbContext.SaveChanges();
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
        public ErrorOr<Created> SavePlayer(Player player)
        {
            var result = _dbContext.Players.OrderByDescending(p => p.Id).FirstOrDefault(p => p.Description == player.Description);

            if (result == null)
            {
                _dbContext.Add(player);
            }
            else
            {
                //player.Id = result.Id + 1;
                //_dbContext.Add(player);
                _dbContext.Update(player);
            }

            _dbContext.SaveChanges();
            return Result.Created;
        }

        public ErrorOr<Created> SaveToInventory(Equipment inn)
        {
            Equipment e = new Equipment
            {
                Name = inn.Name,
                Type = inn.Type,
                Value = inn.Value,
                Description = inn.Description,
                IsConsumable = inn.IsConsumable,
                IsEquipped = inn.IsEquipped,
                IsInInventory = true,
                LegendarySkill = inn.LegendarySkill
            };

            _dbContext.EquipmentInventory.Add(e);
            _dbContext.SaveChanges();
            return Result.Created;
        }

        public Player GetPlayer()
        {
            var result = _dbContext.Players.OrderByDescending(p => p.Id).FirstOrDefault();

            if(result != null)
            {
                List<Ability> list = _dbContext.Abilities.ToList();
                foreach (Ability ability in list)
                    result.ListOfAbilities.Add(ability);

                return result;
            }
            else
                return result;
        }

        public Player GetPlayer(String name)
        {
            return _dbContext.Players.OrderByDescending(p => p.Id).FirstOrDefault(p => p.Description == name);
        }

        public List<Player> GetPlayerList()
        {
            return _dbContext.Players.ToList();
        }

        public List<Quest> GetPlayerQuests()
        {
            Player p = _dbContext.Players.Include(p => p.Quest).FirstOrDefault();
            return p.Quest;
        }

        public List<Quest> GetQuests()
        {
            List<Quest> quests = _dbContext.Quests.OrderByDescending(p => p.Id).ToList();

            /*if (quests.Count == 0)
                CreateQuests();*/

            return quests;
        }

        public List<Equipment> GetStore()
        {
            List<Equipment> store = _dbContext.EquipmentStore.ToList();

            if(store.Count == 0)
                BuildStore();

            return(_dbContext.EquipmentStore.ToList());
        }

        public List<Equipment> GetInventory()
        {
            return _dbContext.EquipmentInventory.Where(e => e.IsInInventory).ToList();
        }

        public List<Ability> GetAbilities()
        {
            return _dbContext.Abilities.OrderByDescending(p => p.Id).ToList();
        }

        public ErrorOr<Deleted> DropTable(String tableName)
        {
            String sql = $"DROP TABLE IF EXISTS {tableName}";
            _dbContext.Database.ExecuteSqlRaw(sql);
            
            return Result.Deleted;
        }

        public void CreateQuests()
        {
            Quest q1 = new Quest
            {
                Title = "1",
                Description = "Quest Number 1",
                IsCompleted = false,
                RewardXP = 50
            };

            Quest q2 = new Quest
            {
                Title = "2",
                Description = "Quest Number 2",
                IsCompleted = false,
                RewardXP = 150
            };

            Quest q3 = new Quest
            {
                Title = "3",
                Description = "Quest Number 3",
                IsCompleted = false,
                RewardXP = 250
            };

            Quest q4 = new Quest
            {
                Title = "4",
                Description = "Quest Number 4",
                IsCompleted = false,
                RewardXP = 100
            };

            _dbContext.Add(q1);
            _dbContext.Add(q2);
            _dbContext.Add(q3);
            _dbContext.Add(q4);

            _dbContext.SaveChanges();
        }

        public void BuildStore()
        {
            Equipment e1 = new Equipment
            {
                Name = "Bronze Sword",
                Type = EquipmentType.Weapon,
                Value = 10,
                LegendarySkill = "Temp",
                Description = "A bronze sword",
            };
            Equipment e2 = new Equipment
            {
                Name = "Common Chest Plate",
                Type = EquipmentType.Armor,
                Value = 5,
                LegendarySkill = "Temp",
                Description = "A regular chest plate",
            };
            Equipment e3 = new Equipment
            {
                Name = "Ring of Strength",
                Type = EquipmentType.Ring,
                Value = 25,
                LegendarySkill = "Temp",
                Description = "A ring giving a boost to player strength",
            };
            Equipment e4 = new Equipment
            {
                Name = "Amulet of Luck",
                Type = EquipmentType.Amulet,
                Value = 20,
                LegendarySkill = "Temp",
                Description = "An amulet giving a boost to player luck",
            };

            _dbContext.EquipmentStore.Add(e1);
            _dbContext.EquipmentStore.Add(e2);
            _dbContext.EquipmentStore.Add(e3);
            _dbContext.EquipmentStore.Add(e4);

            _dbContext.SaveChanges();
        }
    }
}
