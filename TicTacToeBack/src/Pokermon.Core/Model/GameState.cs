using System;
using System.Collections.Generic;
using TicTacToe.Core.Model.Entities;
using TicTacToe.Core.Model.Enums;
using Player = TicTacToe.Core.Model.Enums.Player;

namespace TicTacToe.Core.Model
{
    public class GameState
    {
        public int GameId { get; set; }
        public Guid CirclePlayerId { get; set; }
        public Guid CrossPlayerId { get; set; }
        public bool IsWaitingForPlayers { get; set; } = true;
        public bool IsEndOfGame { get; set; }
        public DateTime? GameEndTime { get; set; }
        public Player PlayersTurn { get; set; } = Player.Circle;
        public TileType[,] Board { get; set; } = new TileType[3, 3];

        public GameState(int id)
        {
            GameId = id;
        }

        public void End()
        {
            IsEndOfGame = true;
            GameEndTime = DateTime.UtcNow;
        }
        public void Restart()
        {
            Board = new TileType[3, 3];
            PlayersTurn = Player.Circle;
            GameEndTime = null;
            IsEndOfGame = false;
        }
    }
}
