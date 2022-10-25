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
        private static Guid? WaitingPlayerId = null;
        private static int _nextGameId = 0;

        public GameState GetGame(int id) =>
            Games.GetValueOrDefault(id);

        public int? GetAwaitingGameId() => WaitingGameId;

        public int CreateWaitingGame(Guid firstPlayerId)
        {
            WaitingPlayerId = firstPlayerId;
            WaitingGameId = _nextGameId;
            return _nextGameId++;
        }

        public void AddGame(int id, GameState newGameState)
        {
            Games[id] = newGameState;
        }

        public IEnumerable<GameState> GetGamesToRestart(DateTime endOfHandTime) =>
            Games.Values.Where(g => g.IsEndOfGame && g.GameEndTime < endOfHandTime);
    }
}
