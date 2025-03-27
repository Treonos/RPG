using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Armor : EquipmentPiece
    {
        public byte ArmorId { get; set; }
        public int Defense { get; set; }
    }
    public static class GlobalArmor
    {
        public static Armor armor1 = new Armor();
    }
}
