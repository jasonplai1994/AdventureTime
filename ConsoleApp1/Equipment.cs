using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Ring,
        Amulet,
        Useable
    }
    public class Equipment
    {
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        public int Value { get; set; } // Monetary value of the item
        public string? LegendarySkill { get; set; } // Special functionality tied to this equipment
        public string? Description { get; set; }
        public bool IsEquipped { get; set; } = false;
    }
}
