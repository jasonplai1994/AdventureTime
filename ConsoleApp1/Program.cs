using ConsoleApp1.Models;
using ConsoleApp1.Models.Equipments;
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
        GameService gameService = new GameService(new GameDbContext());
        bool running = true;
        Game game = new Game();
        var res = "";

        while (res.Equals("")) { 
            res = Login(gameService);

            if(res.Equals(""))
                Console.WriteLine("\t\t\t\t\t*** COULD NOT FIND PLAYER. TRY AGAIN! ***\n");
        };
        Console.ForegroundColor = ConsoleColor.White;

        if (res.Equals("NEW"))
            game.InitializeGame(new Player());
        else
            game.InitializeGame(gameService.GetPlayer(res));

        game.Player.Amulets.Add(game.ListOfAmulets[0]);

        game.Player.Amulets[0].IsEquipped = true;
        // Play the game
        /*Playing(game, running, gameService);

        Console.Clear();
        Console.WriteLine("\n\n\n\t\t\t\t* * * * THANKS FOR PLAYING ADVENTURE TIME! * * * *\n\n\n");*/
    }

    public static void Playing(Game game, bool running, GameService gameService)
    {
        while (running)
        {
            PrintMenu();
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Enter the NPC's name:");
                    var npcName = Console.ReadLine();
                    game.ProcessCommand($"talk {npcName}");
                    break;
                case "2":
                    game.ProcessQuests();
                    gameService.UpdatePlayer(game.Player);
                    break;
                case "3":
                    game.ProcessCompleteQuest();
                    gameService.UpdatePlayer(game.Player);
                    break;
                case "4":
                    game.ProcessBuy();
                    gameService.UpdatePlayer(game.Player);
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
                    game.PrintInventory();
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
                        Console.Write($"\t\t\t[1] Save Exisiting Player {game.Player.Description}\n\t\t\t[OR] Any Key to Exit Without Saving\n\t\t\tResponse: ");
                        var response = Console.ReadLine();
                        bool isValidInput = int.TryParse(response, out int num);
                        if(num == 1)
                        {
                            gameService.UpdatePlayer(game.Player);
                            Console.WriteLine($"\t\t\t\t   Successfully Saved {game.Player.Description}");
                        }
                    }
                    else
                    {
                        Console.Write("\n\n\t\t\tThis is an unsaved player! To save enter a name or leave blank to exit.\n\t\t\tName: ");
                        var name = Console.ReadLine();
                        if(!name.Equals(""))
                        {
                            game.Player.Description = name;
                            Console.WriteLine(gameService.SaveNewPlayer(game.Player));
                            Console.WriteLine($"\t\t\t\t   Successfully Saved {name}");
                        }
                    }
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t    Invalid option. Please Try Again!\n");
                    game.Player.Print();
                    break;
            }
        }
    }

    public static void PrintMenu()
    {
        string str = "\n\t\t\t\t\t - Menu Commands - \n" +
        "\t\t\t\t\t-------------------\n" +
        "\t\t\t\t\t[1]  Talk to NPC\n" +
        "\t\t\t\t\t[2]  Show Quests\n" +
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

    public static string Login(GameService gs)
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




