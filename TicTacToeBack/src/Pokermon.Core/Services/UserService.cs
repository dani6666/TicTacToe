using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.Interfaces.Repositories;
using TicTacToe.Core.Interfaces.Services;
using TicTacToe.Core.Model;

namespace TicTacToe.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid?> Authenticate(UserCredentials userCredentials) => await _userRepository.Authenticate(userCredentials.Username, userCredentials.Password);

        public async Task<bool> ValidatePlayerId(Guid playerId) => await _userRepository.ValidatePlayerId(playerId);
    }
}
