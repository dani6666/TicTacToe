using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.Model.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int WonGames { get; set; } = 0;
        public int LostGames { get; set; } = 0;
        public int DrawGames { get; set; } = 0;
    }
}
