﻿using ConsoleApp1.Services;
using System;
using System.Collections.Generic;

namespace ConsoleApp1;
public enum PeriodOfDay { Morning, Afternoon, Evening, Midnight }

public class Program
{
    private GameService gameService;
    public Program(GameService _gameService) { 
        gameService = _gameService;
    }

    public static void Main(string[] args)
    {
        GameService gameService = new GameService(new GameDbContext());
        //gameService.SaveQuests(Program.CreateQuests());  //running once only
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
                   
                    Console.WriteLine("Enter the quest's name:");
                    var questName = Console.ReadLine();
                    switch (questName)
                    {
                        case "1":
                            game.ProcessCommand($"accept 1 from QuestGiver");
                            break;
                        case "2":
                            game.ProcessCommand($"accept 2 from QuestGiver");
                            break;
                        case "3":
                            game.ProcessCommand($"accept 3 from QuestGiver");
                            break;
                        default: Console.WriteLine("\n\tQuest not available in early access version.\n");
                            break;
                    }
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
                    game.ProcessCommand($"Equip {itemName}");
                    break;

                case "9":
                    Console.WriteLine("Select an item to Unequip");
                    itemName = Console.ReadLine();
                    game.ProcessCommand($"Unequip {itemName}");
                    break;

                case "99":
                    running = false;
                    /*gameService.AddNPC(game.NPCs);
                    gameService.SavePlayer(game.Player);*/
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        game.PrintGameStatus();
    }

    public void ChangeTimePeriod()
    {
        Random rng = new Random();   
    }

    public static List<Quest> CreateQuests()
    {
        Quest q1 = new Quest
        { 
            Title = "1",
            Description = "",
            IsCompleted = false,
            RewardXP = 50 
        };

        Quest q2 = new Quest
        {
            Title = "2",
            Description = "",
            IsCompleted = false,
            RewardXP = 150
        };

        Quest q3 = new Quest
        {
            Title = "3",
            Description = "",
            IsCompleted = false,
            RewardXP = 250
        };

        Quest q4 = new Quest
        {
            Title = "4",
            Description = "",
            IsCompleted = false,
            RewardXP = 100
        };

        return new List<Quest> { q1, q2, q3, q4 };
    }
}




