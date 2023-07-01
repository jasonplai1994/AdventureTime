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

        // Initialize Game and Play
        public void InitializeGame(Player saved, GameService gameService)
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
            Playing(gameService);
        }

        private void Playing(GameService gameService)
        {
            bool running = true;

            while (running)
            {
                PrintMenu();
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                         //TODO
                        break;
                    case "2":
                        ProcessQuests();
                        gameService.UpdatePlayer(Player);
                        break;
                    case "3":
                        ProcessCompleteQuest();
                        gameService.UpdatePlayer(Player);
                        break;
                    case "4":
                        ProcessBuy();
                        gameService.UpdatePlayer(Player);
                        break;
                    case "5":
                        Player.GetBody();
                        break;
                    case "6":
                        ProcessAttack(NPCs.FirstOrDefault(npc => npc.Type == NPCType.Enemy));
                        gameService.UpdatePlayer(Player);
                        break;
                    case "7":
                        PrintInventory();
                        gameService.UpdatePlayer(Player);
                        break;
                    case "8":
                        ProcessEquipItem();
                        gameService.UpdatePlayer(Player);
                        break;
                    case "9":
                        ProcessUnequipItem("");
                        gameService.UpdatePlayer(Player);
                        break;
                    case "99":
                        running = false;
                        if (!Player.Description.Equals("NEW PLAYER"))
                        {
                            Console.Write($"\t\t\t[1] Save Exisiting Player {Player.Description}\n\t\t\t[OR] Any Key to Exit Without Saving\n\t\t\tResponse: ");
                            var response = Console.ReadLine();
                            bool isValidInput = int.TryParse(response, out int num);
                            if (num == 1)
                            {
                                gameService.UpdatePlayer(Player);
                                Console.WriteLine($"\t\t\t\t   Successfully Saved {Player.Description}");
                            }
                        }
                        else
                        {
                            Console.Write("\n\n\t\t\tThis is an unsaved player! To save enter a name or leave blank to exit.\n\t\t\tName: ");
                            var name = Console.ReadLine();
                            if (!name.Equals(""))
                            {
                                Player.Description = name;
                                Console.WriteLine(gameService.SaveNewPlayer(Player));
                                Console.WriteLine($"\t\t\t\t   Successfully Saved {name}");
                            }
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\t\t\t\t    Invalid option. Please Try Again!\n");
                        Player.Print();
                        break;
                }
            }
        }
        
        // Play Turn
        private void PlayTurn(string command)
        {
            // Check if the player is alive
            if (Player.Health <= 0)
            {
                Console.WriteLine("Game Over");
                return;
            }

            // Process the command
            //ProcessCommand(command);

            // Next turn
            TurnNumber++;

            // Time and weather update
            if (TurnNumber % 3 == 0)
            {
                UpdateTimeAndWeather();
            }
        }

        // Update Time and Weather
        private void UpdateTimeAndWeather()
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


        //Game Process Methods
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

        private void ProcessBuy()
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

        private void ProcessCompleteQuest()
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

        private void ProcessQuests()
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

        private void ProcessUseItem(string itemName)  //TODO Possible Use Potions in a fight...
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

        //----------------------------------------------------------------------------------------------------------------------------------------
        //Process Equid and Unequip Methods
        public void ProcessEquipAmulet(Amulet a)
        {
            //TODO Check player level with Amulet Level... Done.
            //TODO When Amulet is Equipped it is removed from the inventory as it is now on the player... Done.
            if(Player.Level < a.LevelRequirement)
            {
                Console.WriteLine($"\n\t\t\tThis item requires a minimum level of {a.LevelRequirement}");
                return;
            }

            if (Player.EquippedAmulet != null)
            {
                Player.EquippedAmulet.IsEquipped = false;
                Player.EquippedAmulet.SetEquipped(Player);
                Player.Amulets.Add(Player.EquippedAmulet);

                Player.EquippedAmulet = a;
                Player.EquippedAmulet.IsEquipped = true;
                Player.EquippedAmulet.SetEquipped(Player);
            }

            if (Player.EquippedAmulet == null)
            {
                Player.EquippedAmulet = a;
                Player.EquippedAmulet.IsEquipped = true;
                Player.EquippedAmulet.SetEquipped(Player);
                Player.Amulets.Remove(a);
            }
        }

        private void ProcessUnequipAmulet()
        {
            
            Player.EquippedAmulet.IsEquipped = false;
            Player.EquippedAmulet.SetEquipped(Player);
            Player.Amulets.Add(Player.EquippedAmulet);
            Player.EquippedAmulet = null;
        }

        private void ProcessEquipRing(Ring a)
        {
            if (Player.Level < a.LevelRequirement)
            {
                Console.WriteLine($"\n\t\t\tThis item requires a minimum level of {a.LevelRequirement}");
                return;
            }

            if (Player.EquippedRing != null)
            {
                Player.EquippedRing.IsEquipped = false;
                Player.EquippedRing.SetEquipped(Player);
                Player.Amulets.Add(Player.EquippedAmulet);

                Player.EquippedRing = a;
                Player.EquippedRing.IsEquipped = true;
                Player.EquippedRing.SetEquipped(Player);
            }

            if (Player.EquippedRing == null)
            {
                Player.EquippedRing = a;
                Player.EquippedRing.IsEquipped = true;
                Player.EquippedRing.SetEquipped(Player);
                Player.Rings.Remove(a);
            }
        }

        public void ProcessUnequipRing()
        {
            Player.EquippedRing.IsEquipped = false;
            Player.EquippedRing.SetEquipped(Player);
            Player.Rings.Add(Player.EquippedRing);
            Player.EquippedRing = null;
        }

        private void ProcessEquipWeapon(Weapon a)
        {
            if (Player.Level < a.LevelRequirement)
            {
                Console.WriteLine($"\n\t\t\tThis item requires a minimum level of {a.LevelRequirement}");
                return;
            }

            if (Player.EquippedWeapon != null)
            {
                Player.EquippedWeapon.IsEquipped = false;
                Player.EquippedWeapon.SetEquipped(Player);
                Player.Weapons.Add(Player.EquippedWeapon);

                Player.EquippedWeapon = a;
                Player.EquippedWeapon.IsEquipped = true;
                Player.EquippedWeapon.SetEquipped(Player);
            }

            if (Player.EquippedWeapon == null)
            {
                Player.EquippedWeapon = a;
                Player.EquippedWeapon.IsEquipped = true;
                Player.EquippedWeapon.SetEquipped(Player);
                Player.Weapons.Remove(a);
            }
        }

        public void ProcessUnequipWeapon()
        {
            Player.EquippedWeapon.IsEquipped = false;
            Player.EquippedWeapon.SetEquipped(Player);
            Player.Weapons.Add(Player.EquippedWeapon);
            Player.EquippedWeapon = null;
        }

        private void ProcessEquipArmors(Armor a)  //TODO In Progress...
        {
            if (Player.Level < a.LevelRequirement)
            {
                Console.WriteLine($"\n\t\t\tThis item requires a minimum level of {a.LevelRequirement}");
                return;
            }

            //TODO


            switch (a.ArmorType)
            {
                case ArmorType.Head:
                    break;
                case ArmorType.Hands:
                    break;
                case ArmorType.Boots:
                    break;
                case ArmorType.Chest:
                    break;
                case ArmorType.Pants:
                    break;
            }
        }

        private void ProcessEquipItem()
        {
            Console.Write("\n\t\t\t\tWhat would you like to equip?\n\n\t\t\t\t[1] Amulet\n\t\t\t\t[2] Ring\n\t\t\t\t[3] Weapon\n\t\t\t\t[4] Armors\n\t\t\t\t  [Or] Any Key to Abort  Response: ");
            var input = Console.ReadLine();
            var isValidInput = int.TryParse(input, out int numb);
            if (numb == 1)
            {

                Console.WriteLine("\n\t\t\t\t\tCurrent Amulets In Inventory" +
                                  "\n\t\t\t\t------------------------------------");
                int i = 0;
                foreach (var item in Player.Amulets)
                    Console.WriteLine($"\t\t\t\t{++i} {item.Name}\n\t\t\t\t- Description: {item.Description}\n\t\t\t\t- Stats: {item.Stats}\n");

                Console.Write("\n\t\t\t\tSelect Amulet to Equip: ");
                input = Console.ReadLine();
                isValidInput = int.TryParse(input, out numb);

                if (numb < 1 || numb > Player.Amulets.Count)
                    return;

                ProcessEquipAmulet(Player.Amulets[numb - 1]);
                Player.Print();
            }

            if (numb == 2)
            {

                Console.WriteLine("\n\t\t\t\t\tCurrent Rings In Inventory" +
                                  "\n\t\t\t\t------------------------------------");
                int i = 0;
                foreach (var item in Player.Rings)
                    Console.WriteLine($"\t\t\t\t{++i} {item.Name}\n\t\t\t\t- Description: {item.Description}\n\t\t\t\t- Stats: {item.Stats}\n");

                Console.Write("\n\t\t\t\tSelect Ring to Equip: ");
                input = Console.ReadLine();
                isValidInput = int.TryParse(input, out numb);

                if (numb < 1 || numb > Player.Rings.Count)
                    return;

                ProcessEquipRing(Player.Rings[numb - 1]);
                Player.Print();
            }

            if (numb == 3)
            {

                Console.WriteLine("\n\t\t\t\t\tCurrent Weapons In Inventory" +
                                  "\n\t\t\t\t------------------------------------");
                int i = 0;
                foreach (var item in Player.Weapons)
                    Console.WriteLine($"\t\t\t\t{++i} {item.Name}\n\t\t\t\t- Description: {item.Description}\n\t\t\t\t- Stats: {item.Stats}\n");

                Console.Write("\n\t\t\t\tSelect Amulet to Equip: ");
                input = Console.ReadLine();
                isValidInput = int.TryParse(input, out numb);

                if (numb < 1 || numb > Player.Weapons.Count)
                    return;

                ProcessEquipWeapon(Player.Weapons[numb - 1]);
                Player.Print();
            }

            if (numb == 4)
            {

                Console.WriteLine("\n\t\t\t\t\tCurrent Armors In Inventory" +
                                  "\n\t\t\t\t------------------------------------");
                int i = 0;
                foreach (var item in Player.Armors)
                    Console.WriteLine($"\t\t\t\t{++i} {item.Name}\n\t\t\t\t- Description: {item.Description}\n\t\t\t\t- Stats: {item.Stats}\n");

                Console.Write("\n\t\t\t\tSelect Amulet to Equip: ");
                input = Console.ReadLine();
                isValidInput = int.TryParse(input, out numb);

                if (numb < 1 || numb > Player.Armors.Count)
                    return;

                ProcessEquipArmors(Player.Armors[numb - 1]);
                Player.Print();
            }
        }

        public void ProcessUnequipItem(string itemName)
        {

            if (Player.EquippedAmulet == null && Player.EquippedAmulet == null && Player.EquippedWeapon == null
                && Player.EquippedBootsArmor == null && Player.EquippedPantsArmor == null && Player.EquippedHeadArmor == null
                && Player.EquippedGlovesArmor == null && Player.EquippedChestArmor == null)
            {
                Console.WriteLine("\n\t\t\t\t\t * No Items Equipped *");
                return;
            }

            int count = 0;
            List<Object> o = new List<object>();

            Console.WriteLine("\n\t\t\t\t\tCurrently Equiped Items\n\t\t\t\t   ---------------------------------");
            count++;
            if(Player.EquippedAmulet != null)
            {
                o.Add(Player.EquippedAmulet);
                Console.WriteLine($"\n\t\t\t\t\t[{count}]  " + Player.EquippedAmulet.Name);
            }
            count++;
            if (Player.EquippedRing != null)
            {
                o.Add(Player.EquippedRing);
                Console.WriteLine($"\n\t\t\t\t\t[{count}]  " + Player.EquippedRing.Name);
            }
            
            count++;
            if (Player.EquippedWeapon != null)
            {
                o.Add(Player.EquippedWeapon);
                Console.WriteLine($"\n\t\t\t\t\t[{count}]  " + Player.EquippedWeapon.Name);
            }
            
            count++;
            if(Player.EquippedBootsArmor != null)
            {
                o.Add(Player.EquippedBootsArmor);
                Console.WriteLine($"\n\t\t\t\t\t[{count}]  " + Player.EquippedBootsArmor.Name);
            }
            
            count++;
            if (Player.EquippedPantsArmor != null)
            {
                o.Add(Player.EquippedPantsArmor);
                Console.WriteLine($"\n\t\t\t\t\t[{count}]  " + Player.EquippedPantsArmor.Name);
            }
            
            count++;
            if (Player.EquippedHeadArmor != null)
            {
                o.Add(Player.EquippedHeadArmor);
                Console.WriteLine($"\n\t\t\t\t\t[{count}]  " + Player.EquippedHeadArmor.Name);
            }
            
            count++;
            if (Player.EquippedGlovesArmor != null)
            {
                o.Add(Player.EquippedGlovesArmor);
                Console.WriteLine($"\n\t\t\t\t\t[{count}]  " + Player.EquippedGlovesArmor.Name);
            }
            
            count++;
            if (Player.EquippedChestArmor != null)
            {
                o.Add(Player.EquippedChestArmor);
                Console.WriteLine($"\n\t\t\t\t\t[{count}]  " + Player.EquippedChestArmor.Name);
            }

            Console.Write("\n\t\t\t\tPlease Selected an Item to UnEquip: ");
            var res = Console.ReadLine();
            bool isValidInput = int.TryParse(res, out int num);
            
            switch (num)
            {
                case 1:
                    ProcessUnequipAmulet();
                    break;
                case 2:
                    ProcessUnequipRing();
                    break;
                case 3:
                    ProcessUnequipRing();
                    break;
                case 4:
                    Player.EquippedBootsArmor.IsEquipped = false;
                    Player.EquippedBootsArmor.SetEquipped(Player);
                    Player.Armors.Add(Player.EquippedBootsArmor);
                    Player.EquippedBootsArmor = null;
                    break;
                case 5:
                    Player.EquippedPantsArmor.IsEquipped = false;
                    Player.EquippedPantsArmor.SetEquipped(Player);
                    Player.Armors.Add(Player.EquippedPantsArmor);
                    Player.EquippedPantsArmor = null;
                    break;
                case 6:
                    Player.EquippedHeadArmor.IsEquipped = false;
                    Player.EquippedHeadArmor.SetEquipped(Player);
                    Player.Armors.Add(Player.EquippedHeadArmor);
                    Player.EquippedHeadArmor = null;
                    break;
                case 7:
                    Player.EquippedGlovesArmor.IsEquipped = false;
                    Player.EquippedGlovesArmor.SetEquipped(Player);
                    Player.Armors.Add(Player.EquippedGlovesArmor);
                    Player.EquippedGlovesArmor = null;
                    break;
                case 8:
                    Player.EquippedChestArmor.IsEquipped = false;
                    Player.EquippedChestArmor.SetEquipped(Player);
                    Player.Armors.Add(Player.EquippedChestArmor);
                    Player.EquippedChestArmor = null;
                    break;
                default:
                    break;

            }
            
            Player.Print();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------
        //PrintMethods
        private void PrintInventory()
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

        private void PrintStore(int num)
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

        private void PrintQuests()
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

        private void PrintMenu()
        {
            string str = "\n\t\t\t\t\t - Menu Commands - \n" +
            "\t\t\t\t\t-------------------\n" +
            "\t\t\t\t\t[1]  Talk to NPC\n" +
            "\t\t\t\t\t[2]  Show Quests\n" +
            "\t\t\t\t\t[3]  Complete Active Quest\n" +
            "\t\t\t\t\t[4]  Buy item\n" +
            "\t\t\t\t\t[5]  See Body\n" +
            "\t\t\t\t\t[6]  Start Battle\n" +
            "\t\t\t\t\t[7]  Open Inventory\n" +
            "\t\t\t\t\t[8]  Equip Item\n" +
            "\t\t\t\t\t[9]  Unequip Item\n" +
            "\t\t\t\t\t[99] Exit game\n";

            Console.Write(str + "\n\t\t\t\t\tResponse: ");
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

        //----------------------------------------------------------------------------------------------------------------------------------------
        //Generate Game Properties Method
        private void GenerateGameProperties()
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
                //TODO FINISH ADDING TYPE TO ALL EQUIPMENTS... Done.
                new Ring
                {
                    Name = "Ring of Dexterity",
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
                new Ring
                {
                    Name = "Ring of Luck",
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
                new Ring
                {
                    Name = "Ring of Intelligence",
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
                new Ring
                {
                    Name = "Ring of Persuasion",
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
                new Ring
                {
                    Name = "Ring of Strength",
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

            //TODO Fix the Types...
            ListOfWeapons = new List<Weapon>
            {
                new Weapon
                {
                    Name = "Wooden Sword",
                    Description = "Simple common sword",
                    Rarity = Rarity.Common,
                    DamageReduction = 0,
                    Stats = 5,
                    Type = 0,
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
                    Type = 0,
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
                    Type = 0,
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
                    Type = 0,
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
                    Type = 0,
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
                    Type = Equipments.Type.Dexterity,
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
                    Type = Equipments.Type.Dexterity,
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
                    Type = Equipments.Type.Dexterity,
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
                    Type = Equipments.Type.Dexterity,
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
                    Type = Equipments.Type.Dexterity,
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
    }
}