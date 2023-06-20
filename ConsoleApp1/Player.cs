﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Player
    {
        public int Health { get; set; } = 20;
        public int XP { get; set; } = 0;
        public int AC { get; set; } = 10; // Default AC value in DnD
        public int Level { get; set; } = 1;
        public string Location { get; set; } = "Start";
        public string Description { get; set; } = "player 1";
        public int Gold { get; set; } = 50;
        public List<string> Inventory { get; set; } = new List<string>();
        public string Quest { get; set; } = "None";
        public Dictionary<string, int> Abilities { get; set; } = new Dictionary<string, int>();

        // Generate Abilities
        public void GenerateAbilities(Random rng)
        {
            //var abilities = new Abilities();

            Abilities.Add("Persuasion", rng.Next(1, 21));
            Abilities.Add("Strength", rng.Next(1, 21));
            Abilities.Add("Intelligence", rng.Next(1, 21));
            Abilities.Add("Dexterity", rng.Next(1, 21));
            Abilities.Add("Luck", rng.Next(1, 21));

            //foreach (KeyValuePair<string, int> kvp in Abilities)
                //Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
        }

        public void Print()
        {
            foreach (KeyValuePair<string, int> kvp in Abilities)
                Console.WriteLine("Character Stats: {0}, Value: {1}", kvp.Key, kvp.Value);

            Console.WriteLine();
            
        }

        public void ExpCheck()
        {
            if (XP >= 100)
            {
                
                Level++;
                XP -= 100;
                Console.WriteLine("\n\t** You leveled Up! **");
            }
        }


    }
}
