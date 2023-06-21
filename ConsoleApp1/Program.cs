using System;
using System.Collections.Generic;

enum PeriodOfDay { Morning, Afternoon, Evening, Midnight }

namespace ConsoleApp1 { 
public class Program
    {
        public static void Main(string[] args)
        {
        
        
            var game=new Game();
            game.InitializeGame();
            game.Player.Print();
            
            Console.Write(game.TimePeriod + " -> " );
            game.UpdateTimeAndWeather();
            Console.WriteLine(game.TimePeriod);

            Console.WriteLine();
            // Play the game
            bool running = true;
            while (running)
            {
                Console.WriteLine("Commands:");

                Console.WriteLine();

                Console.WriteLine("1: Talk to NPC");
                Console.WriteLine("2: Accept quest");
                Console.WriteLine("3: Complete quest");
                Console.WriteLine("4: Buy item");
                Console.WriteLine("5: Use item");
                Console.WriteLine("6: Attack");
                Console.WriteLine("7: Open Inventory");
                Console.WriteLine("8: Equip Item");
                Console.WriteLine("9: Unequip Item");
                Console.WriteLine("99: Exit game");

                Console.Write("\nResponse: ");

                var option = Console.ReadLine();


                switch (option)
                {
                    case "1":
                        Console.WriteLine("Enter the NPC's name:");
                        var npcName = Console.ReadLine();
                        game.ProcessCommand($"talk {npcName}");
                        break;

                    case "2":
                        Console.WriteLine("Enter the quest giver's name:");
                        npcName = Console.ReadLine();
                        Console.WriteLine("Enter the quest's name:");
                        var questName = Console.ReadLine();
                        game.ProcessCommand($"accept {questName} from {npcName}");
                        break;

                    case "3":
                        Console.WriteLine("Enter the quest's name:");
                        questName = Console.ReadLine();
                        game.ProcessCommand($"complete {questName} from QuestGiver");
                        break;

                    case "4":
                        game.PrintStore();
                        Console.Write("Select Item ");
                        var itemName = Console.ReadLine();
                        switch (itemName)
                        {
                            case "1":
                                game.ProcessCommand($"buy HealthPotion from Merchant");
                                break;
                            case "2":
                                game.ProcessCommand($"buy Sword from Merchant");
                                break;
                            case "3":
                                game.ProcessCommand($"buy Armor from Merchant");
                                break;
                            case "4":
                                game.ProcessCommand($"buy Ring from Merchant");
                                break;
                            case "5":
                                game.ProcessCommand($"buy Amulet from Merchant");
                                break;
                            case "6":
                                game.ProcessCommand($"buy Shield from Merchant");
                                break;
                        }                  
                        break;

                    case "5":
                        Console.WriteLine("Enter the item's name: ");
                        itemName = Console.ReadLine();
                        game.ProcessCommand($"use {itemName}");
                        break;

                    case "6":
                        game.ProcessCommand($"attack Enemy");
                        break;

                    case "7":
                        game.ProcessCommand("Inventory");
                        break;

                    case "8":
                        Console.WriteLine("Select an item to Equip: ");
                        itemName = Console.ReadLine();
                        game.ProcessCommand("EquipItem");
                        break;

                    case "9":
                        Console.WriteLine("Select an item to Unequip");
                        itemName = Console.ReadLine();
                        game.ProcessCommand("Unequip");
                        break;

                    case "99":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
            game.PrintGameStatus();
        }

        public void changeTimePeriod()
        {
            Random rng = new Random();   
        }
    }
}




