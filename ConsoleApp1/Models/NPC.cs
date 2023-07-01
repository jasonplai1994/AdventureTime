using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public enum NPCType { Merchant, QuestGiver, Enemy }

    public class NPC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public NPCType Type { get; set; }
        public int Health { get; set; } = 20;
        public int AC { get; set; } = 10; // Default AC value
        public int AttackValue { get; set; } = 5; // Default Attack Value
    }
}
