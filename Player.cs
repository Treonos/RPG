using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Player
    {
        public string Class { get; set; }
        public string Biome { get; set; }
        public string Nickname { get; set; }
    }

    public static class GlobalPlayer
    {
        public static Player player1 = new Player();
    }
}
