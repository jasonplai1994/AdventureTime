using ConsoleApp1.Models.Equipments;
using ConsoleApp1.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Formats.Asn1.AsnWriter;

namespace ConsoleApp1.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Player? Player { get; set; }
        public List<NPC> NPCs { get; set; } = new List<NPC>();
        [NotMapped]
        public List<Quest> ListOfQuests { get; set; } = new List<Quest>();
        [NotMapped]
        public List<Amulet>? ListOfAmulets;
        [NotMapped]
        public List<Ring>? ListOfRings;
        [NotMapped]
        public List<Weapon>? ListOfWeapons;
        [NotMapped]
        public List<Armor>? ListOfArmors;
        [NotMapped]
        public int TurnNumber { get; set; } = 0;
        [NotMapped]
        public PeriodOfDay TimePeriod { get; set; } = PeriodOfDay.Morning;
        [NotMapped]
        public int CurrentDayNumber { get; set; } = 0;
        [NotMapped]
        public string Weather { get; set; } = "Clear";

        public Game()
        {
            GenerateGameProperties();
        }

        // Initialize Abilities
        public void InitializeGame(Player saved)
        {
            Player = saved;
    
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

            Player.Print();
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

            if (parts.Length == 2 && parts[1] == "Enemy")
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
            if (parts.Length == 2)
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
            /*else if (parts.Length == 4)
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
            }*/
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
/*            Console.WriteLine($"Inventory: {string.Join(", ", Player.Inventory)}");
*/            /*Console.WriteLine($"Weapon: {Player.Weapon?.Name ?? "None"}");
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
                var attackRoll = rng.Next(1, 21) + Player.Abilities[4].Stat / 3;

                if (attackRoll >= target.AC)
                {
                    var damage = rng.Next(1, 11) + Player.Abilities[4].Stat / 3; // Assuming weapon damage of 1d10
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

        public void ProcessBuy()
        {
            Console.WriteLine("\n\t\t\t\t\t    Welcome to the General Store\n\t\t\t\tPlease select a specific store you would like to visit\n");
            Console.WriteLine("\t\t\t\t\t\t[1] Weapon Store\n\t\t\t\t\t\t[2] Armor Store\n\t\t\t\t\t\t[3] Amulet Store\n\t\t\t\t\t\t[4] Ring Store\n");
            Console.Write("\t\t\t\t\t\tResponse: ");
            var res = Console.ReadLine();
            var isValidInput = int.TryParse(res, out int num);
            if (num < 1 || num > 4)
            {
                Console.WriteLine("Invalid Option...");
                return;
            }
                
            PrintStore(num);
        }

        public void ProcessCompleteQuest()
        {
            if (Player.Quests.Count < 1)
            {
                Console.WriteLine("\n\t\t\tYou Have No Active Quests!");
                return;
            }
            string str = "\n\t\t\t\t     [Option] Current Quests\n" +
                            "\t\t\t\t-------------------------------------";
            Console.WriteLine(str);

            int count = 0;
            foreach (var item in Player.Quests)
            {
                Console.WriteLine("\n\t\t\t\t  [" + ++count + "]   Title: " + item.Title + "\n\t\t\t\t\tDescription: " + item.Description +
                    "\n\t\t\t\t\tCompleted: " + item.IsCompleted + "\n\t\t\t\t\tExperience: " + item.RewardXP);
            }

            Console.Write("\n\t\t\t    Please Select a Quest to Complete! Response: ");
            var res = Console.ReadLine();
            var isValidInput = int.TryParse(res, out int num);

            if (num < 1 || num > Player.Quests.Count())
            {
                Console.WriteLine("\n\t\t\t    * Input Entered is not an Option on the List *");
                return;
            }

            if (!Player.Quests[num - 1].IsCompleted)
            {
                Player.Quests[num - 1].IsCompleted = true;
                Console.WriteLine($"\n\t\t\t* Quest: {Player.Quests[num - 1].Title} is now marked as completed! Keep Grindin'!");
                Player.XP += Player.Quests[num - 1].RewardXP;
                Player.ExpCheck();
                return;
            }

            Console.WriteLine("\n\t\t\t* It seems the Quest you select was already marked completed! *");
        }

        private void ProcessUseItem(string itemName)
        {
            /*var item = Player.Inventory.FirstOrDefault(i => i.Name == itemName);
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
            }*/
        }

        private void ProcessEquipItem(string itemName)
        {
            /*var item = Player.Inventory.FirstOrDefault(i => i.Name == itemName);
            if (item != null && item is Equipment equipment)
            {
                equipment.IsEquipped = true;
                Console.WriteLine($"\n\t** You equipped {equipment.Name}. **\n");
            }
            else
            {
                Console.WriteLine($"\n\t** You don't have {itemName} in your inventory or it's not a piece of equipment. **\n");
            }*/
        }

        private void ProcessUnequipItem(string itemName)
        {
            /*var item = Player.Inventory.FirstOrDefault(i => i.Name == itemName);
            if (item != null && item.IsEquipped)
            {
                Player.Inventory.Remove(item);
                item.IsEquipped = false;
                Player.Inventory.Add(item);
            }
            Console.WriteLine("\n\t***  You are squishy!  ****\n");*/
        }

        public void PrintInventory()
        {
            if (Player.Weapons.Count == 0 && Player.Armors.Count == 0 && Player.Amulets.Count == 0 && Player.Rings.Count == 0)
            {
                Console.WriteLine("\n\t\t\t** Your inventory is empty. **");
            }
            else
            {
                Console.WriteLine("\n\t\t\t\t\tCurrent Inventory" +
                                  "\n\t\t\t\t------------------------------------");

                foreach (var item in Player.Weapons)
                    Console.WriteLine($"\t\t\t\t- {item.Name}\n\t\t\t\t- Description: {item.Description}\n\t\t\t\t- Stats: {item.Stats}\n");
                foreach (var item in Player.Armors)
                    Console.WriteLine($"\t\t\t\t- {item.Name}\n\t\t\t\t- Description: {item.Description}\n\t\t\t\t- Stats: {item.Stats}\n");
                foreach (var item in Player.Amulets)
                    Console.WriteLine($"\t\t\t\t- {item.Name}\n\t\t\t\t- Description: {item.Description}\n\t\t\t\t- Stats: {item.Stats}\n");
                foreach (var item in Player.Rings)
                    Console.WriteLine($"\t\t\t\t- {item.Name}\n\t\t\t\t- Description: {item.Description}\n\t\t\t\t- Stats: {item.Stats}\n");

                Console.WriteLine();
            }
        }

        private void ProcessAddEquipment()
        {
            /*Player.Inventory.Add(equipment);
            Console.WriteLine($"You received {equipment.Name}. It has been added to your inventory.");*/
        }

        public void ProcessQuests()
        {
            if (Player.Quests.Capacity != 0)
                PrintQuests();
            else
                Console.Write("\n\t\t\t\t  - You Have No Active Quests! -\n");

            Console.Write("\n\t\t\t\tWould you like to add one?\n\n\t\t\t\t[1] Yes  [Or] Any Key to Abort  Response: ");
            var input = Console.ReadLine();
            var isValidInput = int.TryParse(input, out int numb);
            if (numb == 1)
            {
                PrintQuests();
                Console.Write("\n\t\t\t\tPlease Select An Option: ");
                input = Console.ReadLine();
                isValidInput = int.TryParse(input, out int number);

                if (number < 0 || number > ListOfQuests.Count)
                    Console.WriteLine("\n\t\t\t\t* Not a Valid Option! Try Again. *");
                else if (!input.Equals(""))
                {
                    Player.Quests.Add(ListOfQuests[number - 1]);
                    Console.WriteLine("\n\t\t\t\t* Accepted Quest Titled: " + ListOfQuests[number - 1].Title + " *");
                    
                }
            }
        }

        public void GenerateGameProperties()
        {
            ListOfAmulets = new List<Amulet>
            {
                new Amulet
                {
                    Name = "Amulet of Dexterity",
                    Description = "Increases Dexterity Stat",
                    Rarity = Rarity.Common,
                    DamageReduction = 0,
                    Stats = 5,
                    Type = Equipments.Type.Dexterity,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 1,
                    Value = 50,
                },
                new Amulet
                {
                    Name = "Amulet of Luck",
                    Description = "Increases Luck Stat",
                    Rarity = Rarity.Uncommon,
                    DamageReduction = 0,
                    Stats = 10,
                    Type = Equipments.Type.Luck,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 5,
                    Value = 100,
                },
                new Amulet
                {
                    Name = "Amulet of Intelligence",
                    Description = "Increases Intelligence Stat",
                    Rarity = Rarity.Rare,
                    DamageReduction = 0,
                    Stats = 15,
                    Type = Equipments.Type.Intelligence,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 10,
                    Value = 200,
                },
                new Amulet
                {
                    Name = "Amulet of Persuasion",
                    Description = "Increases Persuasion Stat",
                    Rarity = Rarity.Rare,
                    DamageReduction = 0,
                    Stats = 15,
                    Type = Equipments.Type.Persuasion,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 10,
                    Value = 200,
                },
                new Amulet
                {
                    Name = "Amulet of Strength",
                    Description = "Increases strength Stat",
                    Rarity = Rarity.Legendary,
                    DamageReduction = 0,
                    Stats = 20,
                    Type = Equipments.Type.Strength,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 20,
                    Value = 500,
                }
            };

            ListOfRings = new List<Ring>
            {
                //TODO FINISH ADDING TYPE TO ALL EQUIPMENTS
                new Ring
                {
                    Name = "Ring of Dexterity",
                    Description = "Increases Dexterity Stat",
                    Rarity = Rarity.Common,
                    DamageReduction = 0,
                    Stats = 5,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 1,
                    Value = 50,
                },
                new Ring
                {
                    Name = "Ring of Luck",
                    Description = "Increases Luck Stat",
                    Rarity = Rarity.Uncommon,
                    DamageReduction = 0,
                    Stats = 10,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 5,
                    Value = 100,
                },
                new Ring
                {
                    Name = "Ring of Intelligence",
                    Description = "Increases Intelligence Stat",
                    Rarity = Rarity.Rare,
                    DamageReduction = 0,
                    Stats = 15,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 10,
                    Value = 200,
                },
                new Ring
                {
                    Name = "Ring of Persuasion",
                    Description = "Increases Persuasion Stat",
                    Rarity = Rarity.Rare,
                    DamageReduction = 0,
                    Stats = 15,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 10,
                    Value = 200,
                },
                new Ring
                {
                    Name = "Ring of Strength",
                    Description = "Increases strength Stat",
                    Rarity = Rarity.Legendary,
                    DamageReduction = 0,
                    Stats = 20,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 20,
                    Value = 500,
                }
            };

            ListOfWeapons = new List<Weapon>
            {
                new Weapon
                {
                    Name = "Wooden Sword",
                    Description = "Simple common sword",
                    Rarity = Rarity.Common,
                    DamageReduction = 0,
                    Stats = 5,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 1,
                    Value = 50,
                },
                new Weapon
                {
                    Name = "Iron Sword",
                    Description = "Uncommon sword made of iron",
                    Rarity = Rarity.Uncommon,
                    DamageReduction = 0,
                    Stats = 10,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 5,
                    Value = 100,
                },
                new Weapon
                {
                    Name = "Rune Scimitar",
                    Description = "Scimitar made of runic material",
                    Rarity = Rarity.Rare,
                    DamageReduction = 0,
                    Stats = 15,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 10,
                    Value = 200,
                },
                new Weapon
                {
                    Name = "Rune Sword",
                    Description = "Sword made of runic material",
                    Rarity = Rarity.Rare,
                    DamageReduction = 0,
                    Stats = 15,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 10,
                    Value = 200,
                },
                new Weapon
                {
                    Name = "Dragon Sword",
                    Description = "Legendary sword used to slay dragons",
                    Rarity = Rarity.Legendary,
                    DamageReduction = 0,
                    Stats = 20,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 20,
                    Value = 500,
                }
            };

            ListOfArmors = new List<Armor>
            {
                new Armor
                {
                    Name = "Chest Plate",
                    Description = "Common chest plate",
                    ArmorType = ArmorType.Chest,
                    Rarity = Rarity.Common,
                    DamageReduction = 0,
                    Stats = 5,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 1,
                    Value = 50,
                },
                new Armor
                {
                    Name = "Iron Helmet",
                    Description = "A helmet made of iron",
                    ArmorType = ArmorType.Head,
                    Rarity = Rarity.Uncommon,
                    DamageReduction = 0,
                    Stats = 10,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 5,
                    Value = 100,
                },
                new Armor
                {
                    Name = "Thick Gloves",
                    Description = "Rare gloves!",
                    ArmorType = ArmorType.Hands,
                    Rarity = Rarity.Rare,
                    DamageReduction = 0,
                    Stats = 15,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 10,
                    Value = 200,
                },
                new Armor
                {
                    Name = "Thick Pants",
                    Description = "Rare Pants",
                    ArmorType = ArmorType.Pants,
                    Rarity = Rarity.Rare,
                    DamageReduction = 0,
                    Stats = 15,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 10,
                    Value = 200,
                },
                new Armor
                {
                    Name = "Legendary Boots",
                    Description = "Designer jawns",
                    ArmorType = ArmorType.Boots,
                    Rarity = Rarity.Legendary,
                    DamageReduction = 0,
                    Stats = 20,
                    Aspects = "",
                    IsEquipped = false,
                    LevelRequirement = 20,
                    Value = 500,
                }
            };

            ListOfQuests = new List<Quest> 
            {
                new Quest
                {
                    Title = "1",
                    Description = "Quest Number 1",
                    IsCompleted = false,
                    RewardXP = 50
                },
                new Quest
                {
                    Title = "2",
                    Description = "Quest Number 2",
                    IsCompleted = false,
                    RewardXP = 150
                },
                new Quest
                {
                    Title = "3",
                    Description = "Quest Number 3",
                    IsCompleted = false,
                    RewardXP = 250
                },
                new Quest
                {
                    Title = "4",
                    Description = "Quest Number 4",
                    IsCompleted = false,
                    RewardXP = 100
                }
            };
        }

        public void PrintStore(int num)
        {
            int i = 0;
            switch(num)
            {
                case 1:
                    Console.WriteLine("\n\t\t\t\t\t---------------------------");
                    Console.WriteLine("\t\t\t\t\t\tWeapon Store\t");
                    Console.WriteLine("\t\t\t\t\t---------------------------");
                    foreach(var item in ListOfWeapons)
                    {
                        
                        Console.WriteLine($"\t\t\t\t\t  [{++i}]   {item.Name}\n\t\t\t\t\t\t{item.Description}\n\t\t\t\t\t\tStats: " +
                            $"{item.Stats}\n\t\t\t\t\t\tRarity: {item.Rarity}\n\t\t\t\t\t\tCost: {item.Value}\n");
                    }
                    Console.Write($"\n\t\t\t\tYour Current Gold is ${Player.Gold} Select an item to Buy: ");
                    var res = Console.ReadLine();
                    var isValidInput = int.TryParse(res, out num);

                    if (num < 1 || num > ListOfWeapons.Count)
                    {
                        Console.WriteLine("Option not in list...");
                        break;
                    }
                    Player.Weapons.Add(ListOfWeapons[num - 1]);
                    Player.Gold -= ListOfWeapons[num - 1].Value;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\t\t\t\tYou successfully bought {ListOfWeapons[num - 1].Name} for ${ListOfWeapons[num - 1].Value}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 2:
                    Console.WriteLine("\n\t\t\t\t\t----------------------------");
                    Console.WriteLine("\t\t\t\t\t\tArmor Store");
                    Console.WriteLine("\t\t\t\t\t----------------------------");
                    foreach (var item in ListOfArmors)
                    {
                        Console.WriteLine($"\t\t\t\t\t  [{++i}]   {item.Name}\n\t\t\t\t\t\t{item.Description}\n\t\t\t\t\t\tStats: " +
                            $"{item.Stats}\n\t\t\t\t\t\tRarity: {item.Rarity}\n\t\t\t\t\t\tCost: {item.Value}\n");
                    }
                    Console.Write($"\n\t\t\t\tYour Current Gold is ${Player.Gold} Select an item to Buy: ");
                    res = Console.ReadLine();
                    isValidInput = int.TryParse(res, out num);

                    if (num < 1 || num > ListOfArmors.Count)
                    {
                        Console.WriteLine("Option not in list...");
                        break;
                    }
                    Player.Armors.Add(ListOfArmors[num - 1]);
                    Player.Gold -= ListOfArmors[num - 1].Value;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\t\t\t\tYou successfully bought {ListOfArmors[num - 1].Name} for ${ListOfArmors[num - 1].Value}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 3:
                    Console.WriteLine("\n\t\t\t\t\t----------------------------");
                    Console.WriteLine("\t\t\t\t\t\tAmulet Store");
                    Console.WriteLine("\t\t\t\t\t----------------------------");
                    foreach (var item in ListOfAmulets)
                    {
                        Console.WriteLine($"\t\t\t\t\t  [{++i}]   {item.Name}\n\t\t\t\t\t\t{item.Description}\n\t\t\t\t\t\tStats: " +
                            $"{item.Stats}\n\t\t\t\t\t\tRarity: {item.Rarity}\n\t\t\t\t\t\tCost: {item.Value}\n");
                    }
                    Console.Write($"\n\t\t\t\tYour Current Gold is ${Player.Gold} Select an item to Buy: ");
                    res = Console.ReadLine();
                    isValidInput = int.TryParse(res, out num);

                    if (num < 1 || num > ListOfAmulets.Count)
                    {
                        Console.WriteLine("Option not in list...");
                        break;
                    }
                    Player.Amulets.Add(ListOfAmulets[num - 1]);
                    Player.Gold -= ListOfAmulets[num - 1].Value;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\t\t\t\t * You successfully bought {ListOfAmulets[num - 1].Name} for ${ListOfAmulets[num - 1].Value}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 4:
                    Console.WriteLine("\n\t\t\t\t\t----------------------------");
                    Console.WriteLine("\t\t\t\t\t\tRing Store");
                    Console.WriteLine("\t\t\t\t\t----------------------------");
                    foreach (var item in ListOfRings)
                    {
                        Console.WriteLine($"\t\t\t\t\t\t  [{++i}]   {item.Name}\n\t\t\t\t\t\t{item.Description}\n\t\t\t\t\t\tStats: " +
                            $"{item.Stats}\n\t\t\t\t\t\tRarity: {item.Rarity}\n\t\t\t\t\t\tCost: {item.Value}\n");
                    }
                    Console.Write($"\n\t\t\t\tYour Current Gold is ${Player.Gold} Select an item to Buy: ");
                    res = Console.ReadLine();
                    isValidInput = int.TryParse(res, out num);

                    if (num < 1 || num > ListOfRings.Count)
                    {
                        Console.WriteLine("Option not in list...");
                        break;
                    }
                    Player.Rings.Add(ListOfRings[num - 1]);
                    Player.Gold -= ListOfRings[num - 1].Value;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\t\t\t\tYou successfully bought {ListOfRings[num - 1].Name} for ${ListOfRings[num - 1].Value}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        public void PrintQuests()
        {
            if (Player.Quests.Count != 0)
            {
                Console.WriteLine("\n\t\t\t\t- Current Active Quest -");
                foreach (var item in Player.Quests)
                {
                    Console.WriteLine("\t\t\t\tTitle: " + item.Title + "\n\t\t\t\tDescription: " + item.Description +
                                "\n\t\t\t\tCompleted: " + item.IsCompleted + "\n\t\t\t\tExperience: " + item.RewardXP + "\n");
                }
            }

            string str = "\n\t\t\t\t[Option] Current Quests Avaliable\n" +
            "\t\t\t\t-------------------------------------";
            Console.WriteLine(str);

            foreach (var item in ListOfQuests)
            {
                if (Player.Quests.Any(p => p.Description.Equals(item.Description)))
                    continue;

                Console.WriteLine("\n\t\t\t\t  [" + item.Title + "]   Title: " + item.Title + "\n\t\t\t\t\tDescription: " + item.Description +
                    "\n\t\t\t\t\tCompleted: " + item.IsCompleted + "\n\t\t\t\t\tExperience: " + item.RewardXP);
            }
        }
    }
}



