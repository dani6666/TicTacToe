using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Core.Model;

namespace TicTacToe.Core.Interfaces.Services
{
    public interface IPlayersService
    {
        Task<List<PlayerResult>> ListPlayerResults();
    }
}