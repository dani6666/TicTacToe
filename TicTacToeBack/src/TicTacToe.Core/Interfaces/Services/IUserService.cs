using System;
using System.Threading.Tasks;
using TicTacToe.Core.Model;

namespace TicTacToe.Core.Interfaces.Services;

public interface IUserService
{
    Task<Guid?> Authenticate(UserCredentials userCredentials);
    Task<bool> ValidatePlayerId(Guid playerId);
}