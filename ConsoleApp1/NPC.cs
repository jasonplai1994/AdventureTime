using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum NPCType { Merchant, QuestGiver, Enemy }

    public class NPC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NPCType Type { get; set; }
        public int Health { get; set; } = 20;
        public int AC { get; set; } = 10; // Default AC value
        public int AttackValue { get; set; } = 5; // Default Attack Value
        public List<Quest> Quests { get; set; } = new List<Quest>();
        public List<Equipment> Items { get; set; } = new List<Equipment>();
    }
}
