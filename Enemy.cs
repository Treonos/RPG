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
        public string ImageSource { get; set; }
    }

    public static class GlobalEnemy
    {
        public static Enemy enemy1 = new Enemy();
    }
}
