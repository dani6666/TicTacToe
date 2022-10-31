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
        public string CirclePlayerName { get; set; }
        public string CrossPlayerName { get; set; }
        public bool IsWaitingForPlayers { get; set; } = true;
        public bool IsEndOfGame { get; set; }
        public Guid PlayersTurn { get; set; }
        public Guid? Winner { get; set; }
        public TileType[][] Board { get; set; } = new TileType[3][];

        public GameState(int id)
        {
            GameId = id;

            for (var i = 0; i < 3; i++)
                Board[i] = new TileType[3];
        }

        public void End()
        {
            IsEndOfGame = true;
            //GameEndTime = DateTime.UtcNow;
        }
        public void Restart()
        {
            //Board = new TileType[3, 3];
            //PlayersTurn = Player.Circle;
            //GameEndTime = null;
            IsEndOfGame = false;
        }
    }
}
