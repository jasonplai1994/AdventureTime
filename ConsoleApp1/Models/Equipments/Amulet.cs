using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models.Equipments
{
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }

    public enum Type
    {
        Dexterity,
        Intelligence,
        Luck,
        Persuasion,
        Strength
    }

    public class Amulet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Rarity Rarity { get; set; }
        public int DamageReduction { get; set; }
        public int Stats { get; set; }
        public Type Type { get; set; }
        public string Aspects { get; set; }
        public int LevelRequirement { get; set; }
        public int Value { get; set; }
        public bool IsEquipped { get; set; }
        /*public bool IsEquipped
        {
            get => isEquipped;
            set
            {
                if (isEquipped != value)
                {
                    isEquipped = value;
                    if (isEquipped)
                    {
                        // Add the equipment's stat to the player's overall stat
                        for(int i = 0; i < 5; i++)
                        {
                            if(Type.Equals(Player.Abilities[i].Type))
                            {
                                Console.WriteLine("Before: " + Player.Abilities[i].Stat);
                                Player.Abilities[i].Stat += Stats;
                                Console.WriteLine("After: " + Player.Abilities[i].Stat);
                            }
                        }
                        
                    }
                    else
                    {
                        // Subtract the equipment's stat from the player's overall stat
                        for (int i = 0; i < 5; i++)
                        {
                            if (Type.Equals(Player.Abilities[i].Type))
                            {
                                Console.WriteLine("Before: " + Player.Abilities[i].Stat);
                                Player.Abilities[i].Stat -= Stats;
                                Console.WriteLine("After: " + Player.Abilities[i].Stat);
                            }
                        }
                    }
                }
            }
        }*/
        [NotMapped]
        public Player Player { get; set; }

        public void SetEquipped()
        {
            if(IsEquipped)
            {
                // Add the equipment's stat to the player's overall stat
                for (int i = 0; i < 5; i++)
                {
                    if (Type.Equals(Player.Abilities[i].Type))
                    {
                        Console.WriteLine("Before: " + Player.Abilities[i].Stat);
                        Player.Abilities[i].Stat += Stats;
                        Console.WriteLine("After: " + Player.Abilities[i].Stat);
                    }
                }
            }
            else
            {
                // Subtract the equipment's stat from the player's overall stat
                for (int i = 0; i < 5; i++)
                {
                    if (Type.Equals(Player.Abilities[i].Type))
                    {
                        Console.WriteLine("Before: " + Player.Abilities[i].Stat);
                        Player.Abilities[i].Stat -= Stats;
                        Console.WriteLine("After: " + Player.Abilities[i].Stat);
                    }
                }
            }

        }

        public override string ToString()
        {
            return $"{Name}\n{Description}\nStats: {Stats}\nRarity: {Rarity}\nCost: {Value}";
        }

        public bool IsPalindrome(int x)
        {
            string palindrome = "" + x;
            for (int i = 0; i < palindrome.Length; i++)
            {
                if (i == ((palindrome.Length - 1) - i) || i > ((palindrome.Length - 1) - i))
                    break;

                if (palindrome[i].Equals(palindrome[(palindrome.Length - 1) - i]))
                    continue;
                else
                    return false;


            }

            return true;
        }
    }
}

