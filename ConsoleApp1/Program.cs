using ConsoleApp1.Models;
using ConsoleApp1.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace ConsoleApp1;
public enum PeriodOfDay { Morning, Afternoon, Evening, Midnight }

public class Program
{
    public static void Main(string[] args)
    {
        bool running = true;
        GameService gameService = new GameService(new GameDbContext());
        Game game = new Game();
        var res = "";

        while (res.Equals("")) { 
            res = Login(gameService);
            if(res.Equals(""))
                Console.WriteLine("\t\t\t\t\t*** COULD NOT FIND PLAYER. TRY AGAIN! ***\n");
        };
        Console.ForegroundColor = ConsoleColor.White;

        if (res.Equals("NEW"))
            game.InitializeGame(null);
        else
        {
            game.InitializeGame(gameService.GetPlayer(res));
            game.Player.ListOfAbilities = gameService.GetAbilities();
            //gameService.GetPlayerQuests(game.Player);
            game.Player.Quest = gameService.GetQuests();
        }

        game.Player.Print();
        game.Player.Inventory = gameService.GetInventory();  //Load Player Inventory from Database

        // Play the game
        Playing(game, running, gameService);

        Console.Clear();
        Console.WriteLine("\n\n\n\t\t\t\t* * * * THANKS FOR PLAYING ADVENTURE TIME! * * * *\n\n\n");
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
                    if (game.Player.Quest.Capacity != 0)
                        quests = PrintQuests(game.Player.Quest);
                    else
                        Console.Write("\n\t\t\t\t  - You Have No Active Quests! -\n");

                    Console.Write("\n\t\t\t\tWould you like to add one?\n\n\t\t\t\t[1] Yes  [2] No  Response: ");
                    var input = Console.ReadLine();
                    isValidInput = int.TryParse(input, out int numb);
                    if(numb == 1)
                    {
                        quests = PrintQuests(game.Player.Quest);
                        Console.Write("\n\t\t\t\tPlease Select An Option: ");
                        input = Console.ReadLine();
                        isValidInput = int.TryParse(input, out int number);

                        if (number < 0 || number > quests.Count)
                            Console.WriteLine("\n\t\t\t\t* Not a Valid Option! Try Again. *");
                        else if(!input.Equals(""))
                        {
                            game.Player.Quest[numb].IsActive = true;
                            Console.WriteLine("\n\t\t\t\t* Accepted Quest Titled: " + quests[number - 1].Title + " *");
                        }
                    }
                    else
                        continue;
                    break;
                case "3":
                    /*if(game.Player.Quest == null)
                    {
                        Console.WriteLine("\n\t\t\tYou Have No Active Quests!");
                        continue;
                    }

                    Console.WriteLine("\n\t\t\t\t- Current Active Quest -");
                    Console.WriteLine("\t\t\t\tTitle: " + game.Player.Quest.Title + "\n\t\t\t\tDescription: " + game.Player.Quest.Description +
                                        "\n\t\t\t\tCompleted: " + game.Player.Quest.IsCompleted + "\n\t\t\t\tExperience: " + game.Player.Quest.RewardXP);
                    Console.Write("\n\t\t\tWould you like to Complete this Quest?\n\t\t\t[1] Yes  [2] No  Response: ");*/
                    var res = Console.ReadLine();
                    isValidInput = int.TryParse(res, out int num);

                    if (num != 1)
                        continue;

                    //game.ProcessCommand($"complete {game.Player.Quest.Title} from QuestGiver");
                    gameService.SavePlayer(game.Player);
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
                    if (!game.Player.Description.Equals("NEW PLAYER"))
                    {
                        Console.Write($"\t\t\t[1] Save Exisiting Player {game.Player.Description}\n\t\t\t[2] Save As New Player\n\t\t\t[OR] Any Key to Exit\n\t\t\tResponse: ");
                        var response = Console.ReadLine();
                        isValidInput = int.TryParse(response, out num);
                        if(num == 1)
                        {
                            gameService.SavePlayer(game.Player);
                            Console.WriteLine($"\t\t\t\t   Successfully Saved {game.Player.Description}");
                        }
                        if(num == 2)
                        {
                            Console.Write("\n\t\t\t\tEnter Player Name To Save: ");
                            response = Console.ReadLine();
                            while(response.Equals(""))
                            {
                                Console.WriteLine("\t\t\t\tDetected No Input Try Again!");
                                Console.Write("\t\t\t\tEnter Player Name To Save: ");
                                response = Console.ReadLine();
                            }
                            game.Player.Description = response;
                            gameService.SavePlayer(game.Player);
                        }
                    }
                    else
                    {
                        Console.Write("\n\n\t\t\tThis is an unsaved player! To save enter a name or leave blank to exit.\n\t\t\tName: ");
                        var name = Console.ReadLine();
                        if(!name.Equals(""))
                        {
                            game.Player.Description = name;
                            Console.WriteLine(gameService.SavePlayer(game.Player));
                            Console.WriteLine($"\t\t\t\t   Successfully Saved {name}");
                        }
                        else
                            Console.WriteLine("\t\t\t\t* * * UNSAVED * * *");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    public static void PrintMenu()
    {
        String str = "\n\t\t\t\t\t - Menu Commands - \n" +
        "\t\t\t\t\t-------------------\n" +
        "\t\t\t\t\t[1]  Talk to NPC\n" +
        "\t\t\t\t\t[2]  Show Active Quests\n" +
        "\t\t\t\t\t[3]  Complete Active Quest\n" +
        "\t\t\t\t\t[4]  Buy item\n" +
        "\t\t\t\t\t[5]  Use item\n" +
        "\t\t\t\t\t[6]  Start Battle\n" +
        "\t\t\t\t\t[7]  Open Inventory\n" +
        "\t\t\t\t\t[8]  Equip Item\n" +
        "\t\t\t\t\t[9]  Unequip Item\n" +
        "\t\t\t\t\t[99] Exit game\n";

        Console.Write(str + "\n\t\t\t\t\tResponse: ");
       
    }

    public static List<Quest> PrintQuests(List<Quest> playerQuests) 
    {
        int count = 0;

        if(playerQuests.Capacity > 0)
        {
            Console.WriteLine("\n\t\t\t\t- Current Active Quest -");
            foreach (var item in playerQuests)
            {
                if (!item.IsActive)
                    continue;

                Console.WriteLine("\t\t\t\tTitle: " + item.Title + "\n\t\t\t\tDescription: " + item.Description +
                            "\n\t\t\t\tCompleted: " + item.IsCompleted + "\n\t\t\t\tExperience: " + item.RewardXP + "\n");
            }
                
        }

        String str = "\n\t\t\t\t[Option] Current Quests Avaliable\n" +
        "\t\t\t\t-------------------------------------";
        Console.WriteLine(str);

        foreach (var item in playerQuests)
        {
            /*string output = "";
            string result = (playerQuests.Contains(quest)) ? " {In Progress}" : "";*/
            if (item.IsActive)
                continue;

            Console.WriteLine("\n\t\t\t\t  [" + ++count + "]   Title: " + item.Title +  "\n\t\t\t\t\tDescription: " + item.Description +
                "\n\t\t\t\t\tCompleted: " + item.IsCompleted + "\n\t\t\t\t\tExperience: " + item.RewardXP);
        }
        return playerQuests;
    }

    public static String Login(GameService gs)
    {
        List<Player> lst = gs.GetPlayerList();
        String temp = "";
        if (lst.Count > 0)
            temp += "* Saved Data Detected *";
        String str = "\t\t\t*********************************************************************\n" +
                     "\t\t\t*********            Welcome To Adventure Time!             *********\n" +
                     "\t\t\t*********          Please Select From The Options           *********\n" +
                     "\t\t\t*********                                                   *********\n" +
                     "\t\t\t*********              [1] Load Player By Name              *********\n" +
                     "\t\t\t*********                                                   *********\n" +
                     "\t\t\t*********              [2] Create New Player                *********\n" +
                     "\t\t\t*********                                                   *********\n" +
                     "\t\t\t*********************************************************************\n" +
                     $"\t\t\t                      {temp}\n";
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write(str + "\t\t\t\t\t\t    Response: ");
        var response = Console.ReadLine();
        if(response.Equals("1"))
        {
            Console.Write("\t\t\t\t\t    Please Enter Player Name:\n\t\t\t\t\t\t    ");
            response = Console.ReadLine();
            if (response.Equals(""))
            {
                Console.Clear();
                return "";
            }
            var player = gs.GetPlayer(response);
            
            if (player != null)
            {
                Console.Clear();
                return player.Description;
            }
            Console.Clear();
            return "";
        }

        if(response.Equals("2"))
        {
            Console.WriteLine("\n\n\n\t\t\t*********************************************************************");
            Console.WriteLine("\t\t\t*********    New Player Created. Please Save Upon Exit!     *********");
            Console.WriteLine("\t\t\t*********************************************************************");
            
            str = "\t";

            /*Console.Write("\t\t\tGame Start In: ");
            for(int i = 5; i > -1; i--)
            {
                Thread.Sleep(1000);
                Console.Write(i + str);
            }*/
            Console.Clear();
            return "NEW";
        }
        Console.Clear();
        return "";
    }
}




