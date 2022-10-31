using System;
using System.Collections.Generic;
using TicTacToe.Core.Model;

namespace TicTacToe.Core.Interfaces.Repositories
{
    public interface IGamesRepository
    {
        GameState GetGame(int id);
        int? GetAwaitingGameId();
        int CreateWaitingGame(Guid firstPlayerId, string firstPlayerName);
        void StartGame(int id, Guid secondPlayerId, string secondPlayerName);
    }
}