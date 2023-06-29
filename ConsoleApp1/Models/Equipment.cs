using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Ring,
        Amulet
    }

    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        public int Value { get; set; } // Monetary value of the item
        public string? LegendarySkill { get; set; } // Special functionality tied to this equipment
        public string? Description { get; set; }
        public bool IsEquipped { get; set; } = false;
        public bool IsConsumable { get; set; } = false;
        //public bool IsInInventory { get; set; } = false;
    }
}
