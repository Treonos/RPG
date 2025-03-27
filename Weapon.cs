using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Weapon : EquipmentPiece
    {
        public byte WeaponId { get; set; }
        public int Damage { get; set; }
        public int AttackCost { get; set; }
        public float CritChance { get; set; }
        public float CritMultiplier { get; set; }
    }
    public static class GlobalWeapon
    {
        public static Weapon weapon1 = new Weapon();
    }
}
