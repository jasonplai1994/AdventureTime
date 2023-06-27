using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public AbilityType Type { get; set; }
        public int Stat { get; set; }
    }
    
}
