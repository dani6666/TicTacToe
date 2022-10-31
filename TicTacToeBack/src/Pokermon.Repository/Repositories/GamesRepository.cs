using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.Interfaces.Repositories;
using TicTacToe.Core.Model;

namespace TicTacToe.Repository.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private static readonly ConcurrentDictionary<int, GameState> Games = new();
        private static int? WaitingGameId = null;
        private static int _nextGameId = 0;

        public GameState GetGame(int id) =>
            Games.GetValueOrDefault(id);

        public int? GetAwaitingGameId() => WaitingGameId;

        public int CreateWaitingGame(Guid firstPlayerId, string firstPlayerName)
        {
            var game = new GameState(_nextGameId);
            game.CirclePlayerId = firstPlayerId;
            game.CirclePlayerName = firstPlayerName;
            game.IsWaitingForPlayers = true;

            Games.TryAdd(_nextGameId, game);

            WaitingGameId = _nextGameId;
            return _nextGameId++;
        }

        public void StartGame(int id, Guid secondPlayerId, string secondPlayerName)
        {
            WaitingGameId = null;
            var game = Games[id];
            game.CrossPlayerId = secondPlayerId;
            game.CrossPlayerName = secondPlayerName;
            game.PlayersTurn = game.CirclePlayerId;
            game.IsWaitingForPlayers = false;
        }
    }
}
