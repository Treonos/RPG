using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Boots : EquipmentPiece
    {
        public byte BootsId { get; set; }
        public int Speed { get; set; }
        public float DodgeChance { get; set; }

    }
    public static class GlobalBoots
    {
        public static Boots boots1 = new Boots();
    }
}
