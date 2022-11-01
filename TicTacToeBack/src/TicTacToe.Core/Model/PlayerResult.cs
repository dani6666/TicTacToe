using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.Model
{
    public class PlayerResult
    {
        public string Name { get; set; }
        public int WonGames { get; set; }
        public int DrawGames { get; set; }
        public int LostGames { get; set; }

        public PlayerResult(string name, int wonGames, int drawGames, int lostGames)
        {
            Name = name;
            WonGames = wonGames;
            DrawGames = drawGames;
            LostGames = lostGames;
        }
    }
}
