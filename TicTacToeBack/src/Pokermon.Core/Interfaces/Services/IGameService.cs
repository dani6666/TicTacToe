using System;
using TicTacToe.Core.Model;
using TicTacToe.Core.Model.Enums;

namespace TicTacToe.Core.Interfaces.Services
{
    public interface IGameService
    {
        GameState GetGame(int gameId, Guid playerId);
        int GetOrCreateGame(Guid firstPlayerId);
        MoveResult MakeMove(int id, Guid playerId, int moveX, int moveY);
        void CreateNewGame(int id);
        void RestartGames();
    }
}