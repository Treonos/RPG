using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Player
    {
        public string Class { get; set; }
        public string Biome { get; set; }
        public string Nickname { get; set; }

        public int Hp { get; set; }
        public int MaxHp { get; set; }

        public int Mp { get; set; }
        public int MaxMp { get; set; }

        public int Defense { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }

        public float DodgeChance { get; set; }
        public float CritChance { get; set; }
        public float CritMultiplier { get; set; }

        public int Level { get; set; }
        public int CurrentExp { get; set; }
        public int ExpRequired { get; set; }

        public int EquippedArmorId { get; set; }
        public int EquippedWeaponId { get; set; }
        public int EquippedBootsId { get; set; }
    }

    public static class GlobalPlayer
    {
        public static Player player1 = new Player();
    }
}
