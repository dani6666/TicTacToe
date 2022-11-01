using System;
using System.Threading.Tasks;
using TicTacToe.Core.Model;
using TicTacToe.Core.Model.Enums;

namespace TicTacToe.Core.Interfaces.Services
{
    public interface IGameService
    {
        GameStateResponse GetGame(int gameId, Guid playerId);
        Task<int?> GetOrCreateGame(Guid playerId);
        MoveResult MakeMove(int id, Guid playerId, int moveX, int moveY);
    }
}