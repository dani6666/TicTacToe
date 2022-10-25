using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Core.Model;

namespace TicTacToe.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<Guid?> Authenticate(string username, string password);
    Task<bool> ValidatePlayerId(Guid playerId);
    Task<IEnumerable<PlayerResult>> GetAllPlayersResults();
    Task UpdateWinnerResults(Guid playerId);
    Task UpdateLoserResults(Guid playerId);
    Task UpdateDrawResults(Guid firstPlayerId, Guid secondPlayerId);
}