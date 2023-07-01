using ConsoleApp1.Models.Equipments;
using System;
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
        public Amulet EquippedAmulet { get; set; } = null;
        public Ring EquippedRing { get; set; } = null;
        public Weapon EquippedWeapon { get; set; } = null;
        public Armor EquippedHeadArmor { get; set; } = null;
        public Armor EquippedChestArmor { get; set; } = null;
        public Armor EquippedPantsArmor { get; set; } = null;
        public Armor EquippedBootsArmor { get; set; } = null;
        public Armor EquippedGlovesArmor { get; set; } = null;
        public List<Weapon> Weapons { get; set; } = new List<Weapon>();
        public List<Armor> Armors { get; set; } = new List<Armor>();
        public List<Ring> Rings { get; set; } = new List<Ring>();
        public List<Amulet> Amulets { get; set; } = new List<Amulet>();
        public List<Quest> Quests { get; set; } = new List<Quest>();
        public List<Ability> Abilities { get; set; } = new List<Ability>();
        [NotMapped]
        private Random random = new Random();

        public Player()
        {
            GenerateAbilities();
        }

        public void GenerateAbilities()
        {
            Ability a1 = new Ability
            {
                Type = Type.Dexterity,
                Stat = random.Next(1, 21)
            };
            Ability a2 = new Ability
            {
                Type = Type.Intelligence,
                Stat = random.Next(1, 21)
            };
            Ability a3 = new Ability
            {
                Type = Type.Luck,
                Stat = random.Next(1, 21)
            };
            Ability a4 = new Ability
            {
                Type = Type.Persuasion,
                Stat = random.Next(1, 21)
            };
            Ability a5 = new Ability
            {
                Type = Type.Strength,
                Stat = random.Next(10, 21)
            };
            Abilities.Add(a1);
            Abilities.Add(a2);
            Abilities.Add(a3);
            Abilities.Add(a4);
            Abilities.Add(a5);
        }

        public void Print()
        {
            string splash2 = "\t\t\t*********************************************************\n" +
                    $"\t\t\t*                       Welcome                         *\n" +
                    $"\t\t\t*                   Current Level: {Level}             \t*\n" +
                    "\t\t\t*  - Your player was been generated with these stats -  *\n" +
                    "\t\t\t*               - - Character Stats - -                 *\n" +
                    "\t\t\t*               -----------------------                 *\n" +
                    $"\t\t\t*\t\t   Dexterity : {Abilities[0].Stat}\t\t\t*\n" +
                    $"\t\t\t*\t\t   Intelligence : {Abilities[1].Stat}\t\t\t*\n" +
                    $"\t\t\t*\t\t   Luck : {Abilities[2].Stat}\t\t\t\t*\n" +
                    $"\t\t\t*\t\t   Persuasion : {Abilities[3].Stat}\t\t\t*\n" +
                    $"\t\t\t*\t\t   Strength : {Abilities[4].Stat}  \t\t\t*\n" +
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

                    Abilities[0].Stat += random.Next(1, 6);
                    Abilities[1].Stat += random.Next(1, 6);
                    Abilities[2].Stat += random.Next(1, 6);
                    Abilities[3].Stat += random.Next(1, 6);
                    Abilities[4].Stat += random.Next(1, 6);
                }

                string str2 = "\n\t\t*********************************************************\n" +
                    "\t\t*                  - You Leveled Up! -                  *\n" +
                    $"\t\t*           Level [{Level - count}] ----> Current Level: [{Level}]          *\n" +
                    "\t\t*         ----------- Updated Stats ------------        *\n" +
                    $"\t\t*\t\t   Dexterity : {Abilities[0].Stat}\t\t\t*\n" +
                    $"\t\t*\t\t   Intelligence : {Abilities[1].Stat}\t\t\t*\n" +
                    $"\t\t*\t\t   Luck : {Abilities[2].Stat}\t\t\t\t*\n" +
                    $"\t\t*\t\t   Persuasion : {Abilities[3].Stat}\t\t\t*\n" +
                    $"\t\t*\t\t   Strength : {Abilities[4].Stat}\t\t        *\n" +
                    "\t\t*                - - Keep Grinding! - -                 *\n" +
                    "\t\t*********************************************************\n";
                Console.Write(str2);

                return true;
            }
            return false;
        }

        public void GetBody()
        {
            var head = EquippedHeadArmor != null ? EquippedHeadArmor.Name : "No Head Armor";
            var chest = EquippedChestArmor != null ? EquippedChestArmor.Name : "No Chest Armor";
            var gloves = EquippedGlovesArmor != null ? EquippedGlovesArmor.Name : "No Gloves";
            var boots = EquippedBootsArmor != null ? EquippedBootsArmor.Name : "No Boots";
            var pants = EquippedPantsArmor != null ? EquippedPantsArmor.Name : "No Pants Armor";
            var ring = EquippedRing != null ? EquippedRing.Name : "No Ring Equipped";
            var amulet = EquippedAmulet != null ? EquippedAmulet.Name : "No Amulet Equipped";
            var weapon = EquippedWeapon != null ? EquippedWeapon.Name : "No Weapon Equipped";

            string str = $"\n\t\t\t\t\t{head}\n\n\n\t\t\t\t\t{amulet}\n" +
                         $"\n\t\t\t\t\t{chest}\n\n\n\t\t\t{gloves}\t\t\t\t\t{gloves}\n\t\t    {ring}" +
                         $"\n\n\n\n\t\t\t\t\t{pants}\n\n\n\n\n\t\t\t\t{boots}\t\t{boots}";
            Console.WriteLine(str);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
