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
        Game game = new Game();

        game.InitializeGame(Login(gameService), gameService);

        Console.Clear();
        Console.WriteLine("\n\n\n\t\t\t\t* * * * THANKS FOR PLAYING ADVENTURE TIME! * * * *\n\n\n");
    }

    public static Player Login(GameService gs)
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

        var response = "";
        while (response.Equals(""))
        {
            Console.Write(str + "\t\t\t\t\t\t    Response: ");
            response = Console.ReadLine();
            if (response.Equals("1"))
            {
                Console.Write("\t\t\t\t\t    Please Enter Player Name:\n\t\t\t\t\t\t    ");
                response = Console.ReadLine();
                if (response.Equals(""))
                {
                    Console.Clear();
                    continue;
                }
                var player = gs.GetPlayer(response);

                if (player != null)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    return player;
                }
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t*** COULD NOT FIND PLAYER. TRY AGAIN! ***\n");
                response = "";
                continue;
            }

            if (response.Equals("2"))
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
                Console.ForegroundColor = ConsoleColor.White;
                return new Player();
            }
            Console.Clear();
            response = "";
        }

        return new Player();
    }
}




