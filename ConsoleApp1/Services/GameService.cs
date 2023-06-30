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

        /*public List<Equipment> GetGameStore1()
        {
            DbSet<Equipment> equipmentStore = _dbContext.EquipmentStore;
            
            if(equipmentStore.Count() > 0)  //checks if store is already in the DB
                return equipmentStore.ToList();

            equipmentStore.AddRange(GetGameStore());
            _dbContext.SaveChanges();

            return _dbContext.EquipmentStore.ToList();
        }*/
/*
        public List<Quest> GetGameQuests()
        {
            DbSet<Quest> gameQuests = _dbContext.GameQuests;

            if (gameQuests.Count() > 0)  //checks if quests is already in the DB
                return gameQuests.ToList();

            CreateQuests();

            return _dbContext.GameQuests.ToList();
        }*/

        /*public ErrorOr<Created> AddNPC(List<NPC> NPCs)
        {
            int i = 0;

            while (i < NPCs.Count)
            {
                _dbContext.Add(NPCs[i]);
                i++;
            }
            _dbContext.SaveChanges();
            return Result.Created;
        }*/

        /*public ErrorOr<Created> SaveQuests(List<Quest> quests)
        {
            int i = 0;

            while (i < quests.Count)
            {
                _dbContext.Add(quests[i]);
                i++;
            }
            _dbContext.SaveChanges();
            return Result.Created;
        }*/

        public ErrorOr<Created> UpdatePlayer(Player player)
        {
            if (player.Description.Equals("NEW PLAYER"))
                return Result.Created;

            /*foreach (var item in player.PlayerQuests)
                _dbContext.Update(item);

            foreach (var item in player.Inventory)
                _dbContext.Update(item);*/


            _dbContext.Update(player);
            _dbContext.SaveChanges();

            return Result.Created;
        }

        /*public ErrorOr<Created> SavePlayer(Player player)
        {
            if (player.Description.Equals("NEW PLAYER"))
                return Result.Created;

            foreach(var item in player.PlayerQuests)
                _dbContext.Add(item);

            foreach (var item in player.Inventory)
                _dbContext.Add(item);


            _dbContext.Update(player);
            _dbContext.SaveChanges();

            return Result.Created;
        }*/

        public ErrorOr<Created> SaveNewPlayer(Player player)
        {
            _dbContext.Add(player);
            _dbContext.SaveChanges();

            return Result.Created;
        }

        /*public ErrorOr<Created> SaveToInventory(Equipment inn)
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
        }*/

        /*public Player GetPlayer()
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
        }*/

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

       /* public List<Quest> GetPlayerQuests()
        {
            Player p = _dbContext.Players.Include(p => p.Quest).FirstOrDefault();
            return p.Quest;
        }*/

        /*public List<Quest> GetQuests()
        {
            *//*List<Quest> quests = _dbContext.Quests.OrderByDescending(p => p.Id).ToList();
            
            return quests;*//*
            return null;
        }*/

       /* public List<Quest> GetQuests(Player player)
        {
            *//*List<Quest> quests = _dbContext.Quests.OrderByDescending(p => p.Id).ToList();
            //HashSet<Quest> set = new HashSet<Quest>(); does not add duplicates
            List<Quest> result = new List<Quest>();
            foreach (Quest q in quests)
            {
                if(!player.Quest.Contains(q))
                    result.Add(q);
            }*//*

            List<Player> x = _dbContext.Players.Include(p => p.Quest).ToList();
            Console.WriteLine(x[0].ToString());
            Console.WriteLine(x[1].ToString());

            foreach (Player p in x)
                if (p.Description.Equals(player.Description))
                    return p.Quest;

            return null;
        }*/

        /*public List<Equipment> GetStore()
        {
            List<Equipment> store = _dbContext.EquipmentStore.ToList();

            if(store.Count == 0)
                BuildStore();

            return(_dbContext.EquipmentStore.ToList());
        }*/

        /*public List<Equipment> GetInventory()
        {
            return _dbContext.EquipmentInventory.Where(e => e.IsInInventory).ToList();
        }*/

        /*public List<Ability> GetAbilities()
        {
            return _dbContext.Abilities.OrderByDescending(p => p.Id).ToList();
        }*/

        /*public void ClearStore()
        {
            DbSet<Equipment> equipmentStore = _dbContext.EquipmentStore;

            equipmentStore.RemoveRange(equipmentStore);

            int rowsAffected = _dbContext.SaveChanges();
            Console.WriteLine($"Rows affected: {rowsAffected}");
        }*/

        public ErrorOr<Deleted> DropTable(String tableName)
        {
            String sql = $"DROP TABLE IF EXISTS {tableName}";
            _dbContext.Database.ExecuteSqlRaw(sql);
            
            return Result.Deleted;
        }
    }
}
