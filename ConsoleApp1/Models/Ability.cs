using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public enum AbilityType 
    { 
        Dexterity, 
        Intelligence,
        Luck, 
        Persuasion,
        Strength
    }

    public class Ability
    {
        public int Id { get; set; }
        public AbilityType Type { get; set; }
        public int Stat { get; set; }
    }
    
}
