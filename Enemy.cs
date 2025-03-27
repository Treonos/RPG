using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Enemy
    {
        public string Name { get; set; }

        public int Hp { get; set; }
        public int MaxHp { get; set; }

        public int Mp { get; set; }
        public int MaxMp { get; set; }

        public int StatMultiplier { get; set; }

        public int Defense { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }

        public float DodgeChance { get; set; }
        public float CritChance { get; set; }
        public float CritMultiplier { get; set; }

        public int LootMultiplier { get; set; }
    }

    public static class GlobalEnemy
    {
        public static Enemy enemy1 = new Enemy();
    }
}
