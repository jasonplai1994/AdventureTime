﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ConsoleApp1
{
    public class Game
    {
        public int Id { get; set; }
        public List<NPC> NPCs { get; set; } = new List<NPC>();
        public Player Player { get; set; } = new Player();
        public int TurnNumber { get; set; } = 0;
        public PeriodOfDay TimePeriod { get; set; } = PeriodOfDay.Morning;
        public int CurrentDayNumber { get; set; } = 0;
        public string Weather { get; set; } = "Clear";

        // Initialize Abilities
        public void InitializeGame()
        {
            Random rng = new Random();
            Player.GenerateAbilities(rng);

            NPCs.Add(new NPC
            {
                Name = "Merchant",
                Type = NPCType.Merchant,
                Items = new List<Equipment> { 
                    new Equipment { Name = "HealthPotion", Value = 10 },
                    new Equipment { Name = "Weapon", Value = 50 }, 
                    new Equipment { Name = "Armor", Value = 30 },
                    new Equipment { Name = "Ring", Value = 60 },
                    new Equipment { Name = "Amulet", Value = 90 },
                    new Equipment { Name = "Shield", Value = 30 }
                }
            });
            NPCs.Add(new NPC
            {
                Name = "QuestGiver",
                Type = NPCType.QuestGiver,
                Quests = new List<Quest> { new Quest { Title = "1", Description = "Do something", RewardXP = 20 }, new Quest { Title = "2", Description = "Do something", RewardXP = 20 }, new Quest { Title = "3", Description = "Do something", RewardXP = 20 } }
            });
            NPCs.Add(new NPC
            {
                Name = "Enemy",
                Type = NPCType.Enemy

            });
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
            else if(parts.Length == 2 && parts[1] == "Enemy")
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
                else if (action == "Equip") {
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
                    if (Player.Quest == "None")
                    {
                        Console.WriteLine("\nNo Quests Accepted\n");
                    }
                    else
                    {
                        ProcessCompleteQuest(targetItemOrQuest);
                    }
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
            Weather = (TimePeriod == PeriodOfDay.Morning || TimePeriod == PeriodOfDay.Afternoon) ? "Sunny" : "Cloudy";
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
            Console.WriteLine($"Quest: {Player.Quest}");
            Console.WriteLine($"Abilities: {string.Join(", ", Player.Abilities)}");
            /*Console.WriteLine($"Weapon: {Player.Weapon?.Name ?? "None"}");
            Console.WriteLine($"Armor: {Player.Armor?.Name ?? "None"}");
            Console.WriteLine($"Ring: {Player.Ring?.Name ?? "None"}");
            Console.WriteLine($"Amulet: {Player.Amulet?.Name ?? "None"}");*/
        }

        private void ProcessAttack(NPC target)
        {
            Random rng = new Random();
            var attackRoll = rng.Next(1, 21) + Player.Abilities["Strength"] / 3;

            if (attackRoll >= target.AC)
            {
                var damage = rng.Next(1, 11) + Player.Abilities["Strength"] / 3; // Assuming weapon damage of 1d10
                target.Health -= damage;
                Console.WriteLine($"You dealt {damage} damage to {target.Name}. Remaining Enemy Health: " + target.Health);

                if (target.Health <= 0)
                {
                    Console.WriteLine($"You defeated {target.Name}.");

                    Player.XP += 50;

                    Player.ExpCheck(); // Award some XP
                    Console.WriteLine("\n\tExperience: " + Player.XP + " Level: " + Player.Level);
                    Console.WriteLine();

                    target.Health = 20; // monster respawn
                }
                else
                {
                    // Enemy counterattack
                    var enemyAttackRoll = rng.Next(1, 21) + target.AttackValue;

                    if (enemyAttackRoll >= Player.AC)
                    {
                        var enemyDamage = rng.Next(1, 11) + target.AttackValue;
                        Player.Health -= enemyDamage;
                        Console.WriteLine($"{target.Name} dealt {enemyDamage} damage to you. Remaining Player Health: " + Player.Health);
                    }
                    else
                    {
                        Console.WriteLine($"{target.Name} missed.");
                    }
                }
            }
            else
            {
                Console.WriteLine("You missed.");
            }
        }

        private void ProcessBuy(NPC target, string itemName)
        {
            var item = target.Items.FirstOrDefault(i => i.Name == itemName);

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

                equipment.Type = EquipmentType.Armor;
                Player.Inventory.Add(equipment);
                Console.WriteLine($"\nYou bought {equipment.Name}. Gold Remaining: " + Player.Gold);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("You do not have enough gold.");
            }

        }

        // Process Accept Quest
        private void ProcessAcceptQuest(NPC target, string questTitle)
        {
            var quest = target.Quests.FirstOrDefault(q => q.Title == questTitle);
            Player.Quest = quest.Title;
        }

        private void ProcessCompleteQuest(string questname)
        {
            Player.Quest = "None";
            Console.WriteLine("\nCompleted " + questname);

            Player.XP += 20;

            Player.ExpCheck();

            Console.WriteLine("\n\tExperience: " + Player.XP + " Level: " + Player.Level + "\n");
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
                Player.Inventory.Remove( item );
                item.IsEquipped = false;
                Player.Inventory.Add( item );
            }
            Console.WriteLine("\n\t***  You are squishy!  ****\n");
        }

        private void ProcessInventory()
        {
            if (Player.Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                Console.WriteLine("\n\tYour inventory contains: ");

                foreach (var item in Player.Inventory)
                    Console.WriteLine($"\t- {item.Name} ({item.Type})\n\t- Description: {item.Description}\n\t- Value: {item.Value} gold\n");

                Console.WriteLine();
            }
        }

        private void ProcessAddEquipment(Equipment equipment)
        {
            Player.Inventory.Add(equipment);
            Console.WriteLine($"You received {equipment.Name}. It has been added to your inventory.");
        }

        public void PrintStore()
        {
            var item = NPCs.FirstOrDefault(i => i.Type == NPCType.Merchant);
            Console.WriteLine("\n\t**** View Merchandise **** ");
            var count = 1;
            foreach (var i in item.Items)
            {
                Console.WriteLine("\t[" + count + "] " + i.Name + " $" + i.Value);
                count++;
            }
            
            Console.WriteLine();
        }

    }
}
   

