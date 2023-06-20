using System;
using System.Collections.Generic;

enum PeriodOfDay { Morning, Afternoon, Evening, Midnight }

namespace ConsoleApp1 { 
class Program
    {
        static void Main(string[] args)
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
                        Console.Write("Enter the merchant's name: ");
                        npcName = Console.ReadLine();
                        Console.Write("Enter the item's name: ");
                        var itemName = Console.ReadLine();
                        game.ProcessCommand($"buy {itemName} from {npcName}");
                        break;

                    case "5":
                        Console.WriteLine("Enter the item's name: ");
                        itemName = Console.ReadLine();
                        game.ProcessCommand($"use {itemName}");
                        break;

                    case "6":
                       
                        
                        game.ProcessCommand($"attack Enemy");
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




