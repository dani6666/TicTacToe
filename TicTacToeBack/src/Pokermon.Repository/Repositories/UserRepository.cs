using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Core.Interfaces.Repositories;
using TicTacToe.Core.Model;
using TicTacToe.Core.Model.Entities;
using TicTacToe.Core.Model.Enums;

namespace TicTacToe.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<GameContext> _contextFactory;

        private static bool _seeded = false;

        public UserRepository(IDbContextFactory<GameContext> contextFactory)
        {
            _contextFactory = contextFactory;

            if (!_seeded)
            {
                _seeded = true;

                using var context = _contextFactory.CreateDbContext();

                for (int i = 0; i < 100; i++)
                    context.Users.Add(new User { Id = Guid.NewGuid(), Name = $"name{i}", Password = $"password{i}" });

                context.SaveChanges();
            }
        }

        public async Task<Guid?> Authenticate(string username, string password)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var user = context.Users.FirstOrDefault(u => u.Name == username && u.Password == password);

            return user?.Id;
        }

        public async Task<bool> ValidatePlayerId(Guid playerId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            return GetUserById(context, playerId) != null;
        }

        public async Task<IEnumerable<PlayerResult>> GetAllPlayersResults()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            return context.Users.Select(u => new PlayerResult(u.Name, u.WonGames, u.LostGames)).ToList();
        }

        public async Task UpdateWinnerResults(Guid playerId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var user = GetUserById(context, playerId);

            if (user == null)
                throw new ArgumentException();

            user.WonGames++;

            await context.SaveChangesAsync();
        }

        public async Task UpdateLoserResults(Guid playerId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var user = GetUserById(context, playerId);

            if (user == null)
                throw new ArgumentException();

            user.LostGames++;

            await context.SaveChangesAsync();
        }

        public async Task UpdateDrawResults(Guid firstPlayerId, Guid secondPlayerId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var firstUser = GetUserById(context, firstPlayerId);
            var secondUser = GetUserById(context, secondPlayerId);

            if (firstUser == null || secondUser == null)
                throw new ArgumentException();

            firstUser.DrawGames++;
            secondUser.DrawGames++;

            await context.SaveChangesAsync();
        }

        private static User? GetUserById(GameContext context, Guid userId) =>
            context.Users.FirstOrDefault(u => u.Id == userId);
    }
}
