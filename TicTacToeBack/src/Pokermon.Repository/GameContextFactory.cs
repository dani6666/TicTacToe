using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Repository
{
    public class GameContextFactory : IDbContextFactory<GameContext>
    {
        private readonly DbContextOptions<GameContext> _options;

        public GameContextFactory(DbContextOptions<GameContext> options)
        {
            _options = options;
        }

        public GameContext CreateDbContext() => new (_options);
    }
}
