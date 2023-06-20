﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    enum NPCType { Merchant, QuestGiver, Enemy }

    class NPC
    {
        public string Name { get; set; }
        public NPCType Type { get; set; }
        public int Health { get; set; } = 20;
        public int AC { get; set; } = 10; // Default AC value
        public int AttackValue { get; set; } = 5; // Default Attack Value
        public List<Quest> Quests { get; set; } = new List<Quest>();
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
