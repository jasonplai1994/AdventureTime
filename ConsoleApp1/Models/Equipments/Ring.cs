using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models.Equipments
{
    public class Ring
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
        public bool IsEquipped { get; set; } = false;
        public int LevelRequirement { get; set; }
        public int Value { get; set; }
    }
}
