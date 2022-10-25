using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.Model.Enums;

namespace TicTacToe.Core.Model.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        public Guid CirclePlayerId { get; set; }
        public Guid CrossPlayerId { get; set; }
        public bool IsWaitingForPlayers { get; set; } = true;
        public bool IsEndOfGame { get; set; }
        public DateTime? GameEndTime { get; set; }
        public Player PlayersTurn { get; set; }
        public string Board { get; set; }
    }
}
