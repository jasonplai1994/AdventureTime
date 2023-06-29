﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Health { get; set; } = 20;
        public int XP { get; set; } = 0;
        public int AC { get; set; } = 10; // Default AC value in DnD
        public int Level { get; set; } = 1;
        public string Location { get; set; } = "Start";
        public string Description { get; set; } = "NEW PLAYER";
        public int Gold { get; set; } = 100;
        public List<Equipment> Inventory { get; set; } = new List<Equipment>();
        public List<Quest> PlayerQuests { get; set; } = new List<Quest>();
        public List<Ability> ListOfAbilities { get; set; } = new List<Ability>();
        [NotMapped]
        private Random random = new Random();

        // Generate Abilities
        public void GenerateAbilities(Random rng)
        {
            Ability a1 = new Ability
            {
                Type = AbilityType.Dexterity,
                Stat = rng.Next(1, 21)
            };
            Ability a2 = new Ability
            {
                Type = AbilityType.Intelligence,
                Stat = rng.Next(1, 21)
            };
            Ability a3 = new Ability
            {
                Type = AbilityType.Luck,
                Stat = rng.Next(1, 21)
            };
            Ability a4 = new Ability
            {
                Type = AbilityType.Persuasion,
                Stat = rng.Next(1, 21)
            };
            Ability a5 = new Ability
            {
                Type = AbilityType.Strength,
                Stat = rng.Next(10, 21)
            };
            ListOfAbilities.Add(a1);
            ListOfAbilities.Add(a2);
            ListOfAbilities.Add(a3);
            ListOfAbilities.Add(a4);
            ListOfAbilities.Add(a5);
        }

        public void Print()
        {
            string splash2 = "\t\t\t*********************************************************\n" +
                    $"\t\t\t*                       Welcome                         *\n" +
                    $"\t\t\t*                   Current Level: {Level}             \t*\n" +
                    "\t\t\t*  - Your player was been generated with these stats -  *\n" +
                    "\t\t\t*               - - Character Stats - -                 *\n" +
                    "\t\t\t*               -----------------------                 *\n" +
                    $"\t\t\t*\t\t   Dexterity : {ListOfAbilities[0].Stat}\t\t\t*\n" +
                    $"\t\t\t*\t\t   Intelligence : {ListOfAbilities[1].Stat}\t\t\t*\n" +
                    $"\t\t\t*\t\t   Luck : {ListOfAbilities[2].Stat}\t\t\t\t*\n" +
                    $"\t\t\t*\t\t   Persuasion : {ListOfAbilities[3].Stat}\t\t\t*\n" +
                    $"\t\t\t*\t\t   Strength : {ListOfAbilities[4].Stat}  \t\t\t*\n" +
                    "\t\t\t*            - - Good Luck, Have Fun! - -               *\n" +
                    "\t\t\t*********************************************************";

            Console.WriteLine(splash2);
            /*foreach (KeyValuePair<string, int> kvp in Abilities) 
                Console.WriteLine("\t\t*\t\t   {0} : {1}                       *", kvp.Key, kvp.Value);*/
        }

        public bool ExpCheck()
        {
            if (XP >= 100)
            {
                int count = 0;
                while (XP >= 100)
                {
                    count++;
                    Level++;
                    XP -= 100;

                    ListOfAbilities[0].Stat += random.Next(1, 6);
                    ListOfAbilities[1].Stat += random.Next(1, 6);
                    ListOfAbilities[2].Stat += random.Next(1, 6);
                    ListOfAbilities[3].Stat += random.Next(1, 6);
                    ListOfAbilities[4].Stat += random.Next(1, 6);
                }

                string str2 = "\n\t\t*********************************************************\n" +
                    "\t\t*                  - You Leveled Up! -                  *\n" +
                    $"\t\t*           Level [{Level - count}] ----> Current Level: [{Level}]          *\n" +
                    "\t\t*         ----------- Updated Stats ------------        *\n" +
                    $"\t\t*\t\t   Dexterity : {ListOfAbilities[0].Stat}\t\t\t*\n" +
                    $"\t\t*\t\t   Intelligence : {ListOfAbilities[1].Stat}\t\t\t*\n" +
                    $"\t\t*\t\t   Luck : {ListOfAbilities[2].Stat}\t\t\t\t*\n" +
                    $"\t\t*\t\t   Persuasion : {ListOfAbilities[3].Stat}\t\t\t*\n" +
                    $"\t\t*\t\t   Strength : {ListOfAbilities[4].Stat}\t\t        *\n" +
                    "\t\t*                - - Keep Grinding! - -                 *\n" +
                    "\t\t*********************************************************\n";
                Console.Write(str2);

                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
