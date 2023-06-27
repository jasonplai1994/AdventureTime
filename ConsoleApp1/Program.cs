using ConsoleApp1.Models;
using ConsoleApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace ConsoleApp1;
public enum PeriodOfDay { Morning, Afternoon, Evening, Midnight }

public class Program
{
    public static void Main(string[] args)
    {
        GameService gameService = new GameService(new GameDbContext());
        bool running = true;
        Game game = new Game();

        game.InitializeGame(gameService.GetPlayer());
        game.Player.Print();
        game.Player.Inventory = gameService.GetInventory();  //Load Player Inventory from Database

        // Play the game
        Playing(game, running, gameService);
        
        game.PrintGameStatus();
    }

    public static void Playing(Game game, bool running, GameService gameService)
    {
        List<Quest> quests;
        List<Equipment> store;

        while (running)
        {
            PrintMenu();
            var option = Console.ReadLine();
            bool isValidInput;

            switch (option)
            {
                case "1":
                    Console.WriteLine("Enter the NPC's name:");
                    var npcName = Console.ReadLine();
                    game.ProcessCommand($"talk {npcName}");
                    break;
                case "2":
                    if(game.Player.Quest != null)
                    {
                        Console.WriteLine("\n\t\t\t\t- Current Active Quest -");
                        Console.WriteLine("\t\t\t\tTitle: " + game.Player.Quest.Title + "\n\t\t\t\tDescription: " + game.Player.Quest.Description +
                                            "\n\t\t\t\tCompleted: " + game.Player.Quest.IsCompleted + "\n\t\t\t\tExperience: " + game.Player.Quest.RewardXP);
                        quests = PrintQuests(gameService.GetQuests(), game.Player.Quest.Title);

                        Console.Write("\n\t\t\tPress any key to continue...");
                        Console.ReadLine();
                        continue;
                    }
                    if (game.Player.Quest == null)
                    {
                        Console.Write("\n\t\t\tYou Have No Active Quests!\n\t\t\tWould you like to select one?\n\t\t\t[1] Yes  [2] No  Response: ");
                        var input = Console.ReadLine();
                        isValidInput = int.TryParse(input, out int numb);
                        if (numb != 1)
                            continue;
                        quests = PrintQuests(gameService.GetQuests(), "");


                        Console.Write("\n\t\t\tPlease Select An Option: ");
                        input = Console.ReadLine();
                        isValidInput = int.TryParse(input, out int number);
                        if (number < 0 || number > quests.Count)
                            Console.WriteLine("\n\t\t\t* Not a Valid Option! Try Again. *");
                        else
                        {
                            game.Player.Quest = quests[number - 1];
                            Console.WriteLine("\n\t\t\t* Accepted Quest Titled: " + game.Player.Quest.Title + " *");
                        }
                    }

                    break;
                case "3":
                    if(game.Player.Quest == null)
                    {
                        Console.WriteLine("\n\t\t\tYou Have No Active Quests!");
                        continue;
                    }

                    Console.WriteLine("\n\t\t\t\t- Current Active Quest -");
                    Console.WriteLine("\t\t\t\tTitle: " + game.Player.Quest.Title + "\n\t\t\t\tDescription: " + game.Player.Quest.Description +
                                        "\n\t\t\t\tCompleted: " + game.Player.Quest.IsCompleted + "\n\t\t\t\tExperience: " + game.Player.Quest.RewardXP);
                    Console.Write("\n\t\t\tWould you like to Complete this Quest?\n\t\t\t[1] Yes  [2] No  Response: ");
                    var res = Console.ReadLine();
                    isValidInput = int.TryParse(res, out int num);

                    if (num != 1)
                        continue;

                    game.ProcessCommand($"complete {game.Player.Quest.Title} from QuestGiver");
                    break;
                case "4":
                    store = game.PrintStore(gameService.GetStore());
                    Console.Write("\n\t\t\tSelect Item to Buy: ");
                    res = Console.ReadLine();
                    isValidInput = int.TryParse(res, out num);

                    if (num < 0 || num > store.Count)
                        continue;

                    game.Player.Inventory.Add(store[num - 1]);
                    game.Player.Gold -= store[num - 1].Value;
                    gameService.SaveToInventory(store[num - 1]);
                    Console.WriteLine("\n\t\t** You successfully bought " + store[num - 1].Name + " **\n\t\t\tRemaining Gold: $" + game.Player.Gold);
                    break;
                case "5":
                    Console.WriteLine("Enter the item's name: ");
                    var itemName = Console.ReadLine();
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
                    game.ProcessCommand($"Equip {itemName}");
                    break;
                case "9":
                    Console.WriteLine("Select an item to Unequip");
                    itemName = Console.ReadLine();
                    game.ProcessCommand($"Unequip {itemName}");
                    break;
                case "99":
                    running = false;
                    //gameService.SaveInventory(game.Player.Inventory);
                    /*gameService.AddNPC(game.NPCs);*/
                    gameService.SavePlayer(game.Player);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    public static void PrintMenu()
    {
        String str = "\n\t\t\t\t - Menu Commands - \n" +
        "\t\t\t\t-------------------\n" +
        "\t\t\t\t[1]  Talk to NPC\n" +
        "\t\t\t\t[2]  Show Active Quests\n" +
        "\t\t\t\t[3]  Complete Active Quest\n" +
        "\t\t\t\t[4]  Buy item\n" +
        "\t\t\t\t[5]  Use item\n" +
        "\t\t\t\t[6]  Start Battle\n" +
        "\t\t\t\t[7]  Open Inventory\n" +
        "\t\t\t\t[8]  Equip Item\n" +
        "\t\t\t\t[9]  Unequip Item\n" +
        "\t\t\t\t[99] Exit game\n";

        Console.Write(str + "\n\t\t\t\tResponse: ");
    }

    public static List<Quest> PrintQuests(List<Quest> quests, String inprog) 
    {
        int count = 0;
        

        String str = "\n\t\t\t[Option] Current Quests Avaliable\n" +
        "\t\t\t-------------------------------------";
        Console.WriteLine(str);

        foreach (Quest quest in quests)
        {
            string result = (quest.Title.Equals(inprog)) ? " {In Progress}" : "";
            Console.WriteLine("\n\t\t\t  [" + ++count + "]   Title: " + quest.Title + result +  "\n\t\t\t\tDescription: " + quest.Description +
                "\n\t\t\t\tCompleted: " + quest.IsCompleted + "\n\t\t\t\tExperience: " + quest.RewardXP);
        }
            

        return quests;
    }
}




