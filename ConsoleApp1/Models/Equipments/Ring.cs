using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models.Equipments
{
    public class Ring
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public int DamageReduction { get; set; }
        public int Stats { get; set; }
        public string Aspects { get; set; }
        public bool IsEquipped { get; set; } = false;
        public bool IsUseable { get; set; } = true;
        public int Value { get; set; }
    }
}
