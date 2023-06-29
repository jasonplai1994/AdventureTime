using ConsoleApp1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Formats.Asn1.AsnWriter;

namespace ConsoleApp1.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<NPC> NPCs { get; set; } = new List<NPC>();
        [NotMapped]
        public List<Quest> Quests { get; set; } = new List<Quest>();
        [NotMapped]
        public List<Equipment> Store { get; set; } = new List<Equipment>();
        public Player? Player { get; set; }
        public int TurnNumber { get; set; } = 0;
        public PeriodOfDay TimePeriod { get; set; } = PeriodOfDay.Morning;
        public int CurrentDayNumber { get; set; } = 0;
        public string Weather { get; set; } = "Clear";

        // Initialize Abilities
        public void InitializeGame(Player saved, GameService gameService, List<Quest> q, List<Equipment> e)
        {
            if(saved != null)
            {
                Player = saved;
                Store = e;
                Quests = q;
            }
            else
            {
                Player = new Player();
                Random rng = new Random();
                Player.GenerateAbilities(rng);
                Store = e;
                Quests = q;
            }   

            NPCs.Add(new NPC
            {
                Name = "Merchant",
                Type = NPCType.Merchant,
                /*Items = new List<Equipment> {
                    new Equipment { Name = "HealthPotion", Value = 10 },
                    new Equipment { Name = "Weapon", Value = 50 },
                    new Equipment { Name = "Armor", Value = 30 },
                    new Equipment { Name = "Ring", Value = 60 },
                    new Equipment { Name = "Amulet", Value = 90 },
                    new Equipment { Name = "Shield", Value = 30 }
                }*/
            });
            NPCs.Add(new NPC
            {
                Name = "QuestGiver",
                Type = NPCType.QuestGiver,
                //Quests = new List<Quest> { new Quest { Title = "1", Description = "Do something", RewardXP = 20 }, new Quest { Title = "2", Description = "Do something", RewardXP = 20 }, new Quest { Title = "3", Description = "Do something", RewardXP = 20 } }
            });
            NPCs.Add(new NPC
            {
                Name = "Enemy",
                Type = NPCType.Enemy

            });

            //Store = gameService.GetGameStore();
            //Quests = gameService.GetGameQuests();
        }

        // Play Turn
        public void PlayTurn(string command)
        {
            // Check if the player is alive
            if (Player.Health <= 0)
            {
                Console.WriteLine("Game Over");
                return;
            }

            // Process the command
            ProcessCommand(command);

            // Next turn
            TurnNumber++;

            // Time and weather update
            if (TurnNumber % 3 == 0)
            {
                UpdateTimeAndWeather();
            }
        }

        // Process Command
        public void ProcessCommand(string command)
        {
            // Implement your own game logic
            var parts = command.Split(' ');

            if (parts.Length == 1)
            {
                var action = parts[0];

                if (action == "Inventory")
                    ProcessInventory();

                return;
            }
            else if (parts.Length == 2 && parts[1] == "Enemy")
            {
                var target = NPCs.FirstOrDefault(npc => npc.Name == parts[1]);
                var action = parts[0];

                if (target == null)
                {
                    Console.WriteLine("Invalid target");
                }
                if (action == "attack" && target.Type == NPCType.Enemy)
                {
                    ProcessAttack(target);
                }
                return;
            }
            else if (parts.Length == 2)
            {
                var action = parts[0];
                var targetName = parts[1];

                if (action == "use" && Player.Health < 20)
                {
                    ProcessUseItem(targetName);
                }
                else if (action == "talk")
                {
                    Console.WriteLine("Hello " + targetName);
                }
                else if (action == "Equip")
                {
                    ProcessEquipItem(targetName);
                }
                else if (action == "Unequip")
                {
                    ProcessUnequipItem(targetName);
                }
                else
                {
                    Console.WriteLine("Invalid action");
                }
                return;
            }
            else if (parts.Length == 4)
            {
                var action = parts[0];
                var targetName = parts[3];
                var targetItemOrQuest = parts[1];

                var target = NPCs.FirstOrDefault(npc => npc.Name == targetName);

                if (target == null)
                {
                    Console.WriteLine("Invalid target");
                }
                else if (action == "buy" && target.Type == NPCType.Merchant)
                {
                    ProcessBuy(target, targetItemOrQuest);

                }
                else if (action == "accept" && target.Type == NPCType.QuestGiver)
                {
                    ProcessAcceptQuest(target, targetItemOrQuest);
                    Console.WriteLine($"You accepted Quest {targetItemOrQuest} from QuestGiver");
                }
                else if (action == "complete")
                {
                    ProcessCompleteQuest(targetItemOrQuest);
                }
                else
                {
                    Console.WriteLine("Invalid action");
                }
                return;
            }
        }

        // Update Time and Weather
        public void UpdateTimeAndWeather()
        {
            switch (TimePeriod)
            {
                case PeriodOfDay.Morning:
                    TimePeriod = PeriodOfDay.Afternoon;
                    break;
                case PeriodOfDay.Afternoon:
                    TimePeriod = PeriodOfDay.Evening;
                    break;
                case PeriodOfDay.Evening:
                    TimePeriod = PeriodOfDay.Midnight;
                    break;
                case PeriodOfDay.Midnight:
                    TimePeriod = PeriodOfDay.Morning;
                    CurrentDayNumber++;
                    break;
            }
            // Update the weather (you can implement more advanced weather system)
            Weather = TimePeriod == PeriodOfDay.Morning || TimePeriod == PeriodOfDay.Afternoon ? "Sunny" : "Cloudy";
        }

        public void PrintGameStatus()
        {
            Console.WriteLine($"Turn: {TurnNumber}");
            Console.WriteLine($"Time of day: {TimePeriod}");
            Console.WriteLine($"Current day: {CurrentDayNumber}");
            Console.WriteLine($"Weather: {Weather}");
            Console.WriteLine($"Health: {Player.Health}");
            Console.WriteLine($"XP: {Player.XP}");
            Console.WriteLine($"AC: {Player.AC}");
            Console.WriteLine($"Level: {Player.Level}");
            Console.WriteLine($"Location: {Player.Location}");
            Console.WriteLine($"Description: {Player.Description}");
            Console.WriteLine($"Gold: {Player.Gold}");
            Console.WriteLine($"Inventory: {string.Join(", ", Player.Inventory)}");
            /*Console.WriteLine($"Weapon: {Player.Weapon?.Name ?? "None"}");
            Console.WriteLine($"Armor: {Player.Armor?.Name ?? "None"}");
            Console.WriteLine($"Ring: {Player.Ring?.Name ?? "None"}");
            Console.WriteLine($"Amulet: {Player.Amulet?.Name ?? "None"}");*/
        }

        private void ProcessAttack(NPC target)
        {
            Console.WriteLine("\n\n\t\t\t\t- Commence Battle -");
            Console.WriteLine($"\n\t\t\t[6]  Attack\t\t\tPlayer Health: {Player.Health}\n\t\t\t[OR] Any key to exit\t\tEnemy Health: {target.Health}");
            Console.Write("\t\t\tResponse: ");
            var response = Console.ReadLine();

            while (target.Health > 1 && response.Equals("6"))
            {
                Random rng = new Random();
                var attackRoll = rng.Next(1, 21) + Player.ListOfAbilities[4].Stat / 3;

                if (attackRoll >= target.AC)
                {
                    var damage = rng.Next(1, 11) + Player.ListOfAbilities[4].Stat / 3; // Assuming weapon damage of 1d10
                    target.Health -= damage;
                    Console.WriteLine($"\n\t\t\tYou dealt {damage} damage to {target.Name}.");

                    if (target.Health <= 0)
                    {
                        Console.WriteLine($"\n\t\t\t* * !You defeated {target.Name}! * *");

                        Player.XP += 50;

                        if (!Player.ExpCheck()) // Award some XP
                            Console.WriteLine("\n\t\t\tCurrent Exp: " + Player.XP + " Current Level: " + Player.Level);

                        Console.WriteLine();

                        target.Health = 20; // monster respawn
                        Player.Health = 20;
                        return;
                    }
                    else
                    {
                        // Enemy counterattack
                        var enemyAttackRoll = rng.Next(1, 21) + target.AttackValue;

                        if (enemyAttackRoll >= Player.AC)
                        {
                            var enemyDamage = rng.Next(1, 11) + target.AttackValue;
                            Player.Health -= enemyDamage;
                            Console.WriteLine($"\t\t\t{target.Name} dealt {enemyDamage} damage to you.");
                        }
                        else
                        {
                            Console.WriteLine($"\n\t\t\t\t* {target.Name} missed *");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n\t\t\t\t* You missed *");
                }

                Console.WriteLine($"\n\t\t\t[6]  Attack Again \t\tPlayer Health: {Player.Health}\n\t\t\t[OR] Any key to exit\t\tEnemy Health: {target.Health}");
                Console.Write("\t\t\tResponse: ");
                response = Console.ReadLine();
            }
            Console.WriteLine("\n\t\t\t\t- Exiting Battle -\n\n");
            target.Health = 20;
            Player.Health = 20;
        }

        private void ProcessBuy(NPC target, string itemName)
        {
            /*//var item = target.Items.FirstOrDefault(i => i.Name == itemName);

            if (item == null)
            {
                Console.WriteLine("Item not found");
                return;
            }

            if (Player.Gold >= item.Value)
            {
                Player.Gold -= item.Value;
                Equipment equipment = new Equipment();
                equipment.Name = itemName;
                equipment.Value = item.Value;

                switch (itemName)
                {
                    case "Weapon":
                        equipment.Type = EquipmentType.Weapon;
                        break;
                    case "Armor":
                        equipment.Type = EquipmentType.Armor;
                        break;
                    case "Ring":
                        equipment.Type = EquipmentType.Ring;
                        break;
                    case "Amulet":
                        equipment.Type = EquipmentType.Amulet;
                        break;
                }
                equipment.IsInInventory = true;
                Player.Inventory.Add(equipment);
                Console.WriteLine($"\nYou bought {equipment.Name}. Gold Remaining: " + Player.Gold);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("You do not have enough gold.");
            }*/

        }

        // Process Accept Quest
        private void ProcessAcceptQuest(NPC target, string questTitle)
        {
            //var quest = target.Quests.FirstOrDefault(q => q.Title == questTitle);
            //Player.Quest = quest.Title;
        }

        private void ProcessCompleteQuest(string questname)
        {
            /*bool q = Player.Quest.Contains();
            
            Player.Quest = null;
            Console.WriteLine("\n\t\t\t** Completed Quest Title: " + questname + " **");

            Player.XP += q.RewardXP;

            if (!Player.ExpCheck())
                Console.WriteLine("\n\t\tExperience: " + Player.XP + " Level: " + Player.Level + "\n");*/
        }

        private void ProcessUseItem(string itemName)
        {
            var item = Player.Inventory.FirstOrDefault(i => i.Name == itemName);
            if (item != null)
            {
                Player.Inventory.Remove(item);

                if (item.Name == "HealthPotion")
                {
                    Player.Health = Math.Min(20, Player.Health + 10);
                    Console.WriteLine("\nYou used a Health Potion and recovered 10 health. Player Health: " + Player.Health);
                }
                // Process other types of items...
            }
            else
            {
                Console.WriteLine("\nYou do not have that item in your inventory.");
            }
        }

        private void ProcessEquipItem(string itemName)
        {
            var item = Player.Inventory.FirstOrDefault(i => i.Name == itemName);
            if (item != null && item is Equipment equipment)
            {
                equipment.IsEquipped = true;
                Console.WriteLine($"\n\t** You equipped {equipment.Name}. **\n");
            }
            else
            {
                Console.WriteLine($"\n\t** You don't have {itemName} in your inventory or it's not a piece of equipment. **\n");
            }
        }

        private void ProcessUnequipItem(string itemName)
        {
            var item = Player.Inventory.FirstOrDefault(i => i.Name == itemName);
            if (item != null && item.IsEquipped)
            {
                Player.Inventory.Remove(item);
                item.IsEquipped = false;
                Player.Inventory.Add(item);
            }
            Console.WriteLine("\n\t***  You are squishy!  ****\n");
        }

        private void ProcessInventory()
        {
            if (Player.Inventory.Count == 0)
            {
                Console.WriteLine("\n\t\t\t** Your inventory is empty. **");
            }
            else
            {
                Console.WriteLine("\n\t\t\t\tCurrent Inventory" +
                                  "\n\t\t\t------------------------------------");

                foreach (var item in Player.Inventory)
                    Console.WriteLine($"\t\t\t- {item.Name} ({item.Type})\n\t\t\t- Description: {item.Description}\n\t\t\t- Value: {item.Value} gold\n");

                Console.WriteLine();
            }
        }

        private void ProcessAddEquipment(Equipment equipment)
        {
            Player.Inventory.Add(equipment);
            Console.WriteLine($"You received {equipment.Name}. It has been added to your inventory.");
        }

        public List<Equipment> PrintStore(List<Equipment> store)
        {
            var item = NPCs.FirstOrDefault(i => i.Type == NPCType.Merchant);
            string str = "\n\t\t\t*******************************\n\t\t\t*        General Store        *" +
                "\n\t\t\t*       Merchant  Items       *" +
                $"\n\t\t\t*       Current Gold: ${Player.Gold}     *" +
                "\n\t\t\t*******************************";
            Console.WriteLine(str);
            var count = 1;

            foreach (Equipment e in store)
            {
                Console.WriteLine("\t\t\t[" + count + "] \t" + e.Name + "   $" + e.Value);
                count++;
            }

            Console.WriteLine();

            return store;
        }

    }
}


