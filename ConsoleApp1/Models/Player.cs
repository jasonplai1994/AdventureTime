using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int Health { get; set; } = 20;
        public int XP { get; set; } = 0;
        public int AC { get; set; } = 10; // Default AC value in DnD
        public int Level { get; set; } = 1;
        public string Location { get; set; } = "Start";
        public string Description { get; set; } = "Player 1";
        public int Gold { get; set; } = 50;
        [NotMapped]
        public List<Equipment> Inventory { get; set; } = new List<Equipment>();
        public Quest? Quest { get; set; }
        [NotMapped]
        public Dictionary<string, int> Abilities { get; set; } = new Dictionary<string, int>();
        public List<Ability> ListOfAbilities { get; set; } = new List<Ability> { };
        [NotMapped]
        private Random random = new Random();
        /*[NotMapped]
        public Equipment Weapon { get; set; }
        [NotMapped]
        public Equipment Armor { get; set; }
        [NotMapped]
        public Equipment Ring { get; set; }
        [NotMapped]
        public Equipment Amulet { get; set; }
        [NotMapped]
        public Equipment Useable { get; set; }*/

        // Generate Abilities
        public void GenerateAbilities(Random rng)
        {
            /*Abilities.Add("Persuasion", rng.Next(1, 21));
            Abilities.Add("Strength", rng.Next(10, 21));
            Abilities.Add("Intelligence", rng.Next(1, 21));
            Abilities.Add("Dexterity", rng.Next(1, 21));
            Abilities.Add("Luck", rng.Next(1, 21));*/

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
            /*string splash = "\t\t*********************************************************\n" +
                    "\t\t*               Welcome to Adventure Time!              *\n" +
                    "\t\t*  - Your player was been generated with these stats -  *\n" +
                    "\t\t*               - - Character Stats - -                 *\n" +
                    "\t\t*               -----------------------                 *\n" +
                    $"\t\t*\t\t   Dexterity : {Abilities["Dexterity"]}\t\t\t*\n" +
                    $"\t\t*\t\t   Intelligence : {Abilities["Intelligence"]}\t\t\t*\n" +
                    $"\t\t*\t\t   Luck : {Abilities["Luck"]}\t\t\t\t*\n" +
                    $"\t\t*\t\t   Persuasion : {Abilities["Persuasion"]}\t\t\t*\n" +
                    $"\t\t*\t\t   Strength : {Abilities["Strength"]}\t\t        *\n" +
                    "\t\t*            - - Good Luck, Have Fun! - -               *\n" +
                    "\t\t*********************************************************";*/

            string splash2 = "\t\t*********************************************************\n" +
                    "\t\t*               Welcome to Adventure Time!              *\n" +
                    "\t\t*  - Your player was been generated with these stats -  *\n" +
                    "\t\t*               - - Character Stats - -                 *\n" +
                    "\t\t*               -----------------------                 *\n" +
                    $"\t\t*\t\t   Dexterity : {ListOfAbilities[0].Stat}\t\t\t*\n" +
                    $"\t\t*\t\t   Intelligence : {ListOfAbilities[1].Stat}\t\t\t*\n" +
                    $"\t\t*\t\t   Luck : {ListOfAbilities[2].Stat}\t\t\t\t*\n" +
                    $"\t\t*\t\t   Persuasion : {ListOfAbilities[3].Stat}\t\t\t*\n" +
                    $"\t\t*\t\t   Strength : {ListOfAbilities[4].Stat}\t\t        *\n" +
                    "\t\t*            - - Good Luck, Have Fun! - -               *\n" +
                    "\t\t*********************************************************";

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

                /*Abilities.TryGetValue("Persuasion", out int Persuasion);
                Persuasion += random.Next(1, 6);
                Abilities.Remove("Persuasion");
                Abilities.Add("Persuasion", Persuasion);

                Abilities.TryGetValue("Strength", out int Strength);
                Strength += random.Next(1, 6);
                Abilities.Remove("Strength");
                Abilities.Add("Strength", Strength);

                Abilities.TryGetValue("Intelligence", out int Intelligence);
                Intelligence += random.Next(1, 6);
                Abilities.Remove("Intelligence");
                Abilities.Add("Intelligence", Intelligence);

                Abilities.TryGetValue("Dexterity", out int Dexterity);
                Dexterity += random.Next(1, 6);
                Abilities.Remove("Dexterity");
                Abilities.Add("Dexterity", Dexterity);

                Abilities.TryGetValue("Luck", out int Luck);
                Luck += random.Next(1, 6);
                Abilities.Remove("Luck");
                Abilities.Add("Luck", Luck);*/

                /*string str = "\n\t\t*********************************************************\n" +
                    "\t\t*                  - You Leveled Up! -                  *\n" +
                    $"\t\t*           Level [{Level - count}] ----> Current Level: [{Level}]          *\n" +
                    "\t\t*         ----------- Updated Stats ------------        *\n" +
                    $"\t\t*\t\t   Dexterity : {Abilities["Dexterity"]}\t\t\t*\n" +
                    $"\t\t*\t\t   Intelligence : {Abilities["Intelligence"]}\t\t\t*\n" +
                    $"\t\t*\t\t   Luck : {Abilities["Luck"]}\t\t\t\t*\n" +
                    $"\t\t*\t\t   Persuasion : {Abilities["Persuasion"]}\t\t\t*\n" +
                    $"\t\t*\t\t   Strength : {Abilities["Strength"]}\t\t        *\n" +
                    "\t\t*                - - Keep Grinding! - -                 *\n" +
                    "\t\t*********************************************************\n";*/

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
    }
}
