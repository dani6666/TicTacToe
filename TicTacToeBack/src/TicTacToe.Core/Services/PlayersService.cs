using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Interfaces.Repositories;
using TicTacToe.Core.Interfaces.Services;
using TicTacToe.Core.Model;

namespace TicTacToe.Core.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly IUserRepository _userRepository;

        public PlayersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<PlayerResult>> ListPlayerResults()
        {
            return (await _userRepository.GetAllPlayersResults()).OrderByDescending(p => (p.WonGames, p.DrawGames, p.LostGames)).ToList();
        }
    }
}
