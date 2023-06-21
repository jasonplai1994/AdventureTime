using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Game
    {
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
            // Player player = new Player();
            Player.GenerateAbilities(rng);



            NPCs.Add(new NPC
            {
                Name = "Merchant",
                Type = NPCType.Merchant,
                Items = new List<Item> { new Item { Name = "HealthPotion", Price = 10 } }
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
            Console.WriteLine(parts.Length);

            if (parts.Length == 2)
            {
                var action = parts[0];
                var targetName = parts[1];

                if (action == "use" && Player.Health < 20)
                {
                    ProcessUseItem(targetName);
                    return;
                }
                var target = NPCs.FirstOrDefault(npc => npc.Name == targetName);

                if (target == null)
                {
                    Console.WriteLine("Invalid target");
                    //return;
                }
                if (action == "attack" && target.Type == NPCType.Enemy)
                {
                    ProcessAttack(target);
                }
                else if (action == "talk")
                {
                    Console.WriteLine("Hello " + target.Name);
                }
                else
                {
                    Console.WriteLine("Invalid action");
                }
                return;


            }

            // Example of command: "buy Health Potion from Merchant"
            //var parts = command.Split(' ');

            if (parts.Length != 4)
            {
                Console.WriteLine("Invalid command");
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
                    return;
                }
                if (action == "buy" && target.Type == NPCType.Merchant)
                {
                    ProcessBuy(target, targetItemOrQuest);
                }
                else if (action == "accept" && target.Type == NPCType.QuestGiver)
                {
                    ProcessAcceptQuest(target, targetItemOrQuest);
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

            if (Player.Gold >= item.Price)
            {
                Player.Gold -= item.Price;
                Player.Inventory.Add(item.Name);
                Console.WriteLine($"\nYou bought {item.Name}. Gold Remaining: " + Player.Gold);
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

            if (quest == null)
            {
                Console.WriteLine("Quest not found");
                return;
            }

            Player.Quest = quest.Title;
            Console.WriteLine($"\nYou accepted the quest: {quest.Title}.");

        }

        private void ProcessCompleteQuest(string questname)
        {
            Player.Quest = "None";
            Console.WriteLine("\nCompleted " + questname);

            Player.XP += 20;

            Player.ExpCheck();

            Console.WriteLine("\n\tExperience: " + Player.XP + " Level: " + Player.Level);

            Console.WriteLine();

        }

        private void ProcessUseItem(string itemName)
        {
            if (Player.Inventory.Contains(itemName))
            {
                Player.Inventory.Remove(itemName);

                if (itemName == "HealthPotion")
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
    }
}
