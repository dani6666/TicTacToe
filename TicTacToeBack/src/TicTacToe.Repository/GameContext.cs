using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Core.Model.Entities;

namespace TicTacToe.Repository
{
    public class GameContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        //public DbSet<Game> Games { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasData(GetSeedUsers());

            base.OnModelCreating(modelBuilder);
        }

        private IEnumerable<User> GetSeedUsers()
        {
            for (int i = 0; i < 100; i++)
                yield return new User { Id = Guid.NewGuid(), Name = $"name{i}", Password = $"password{i}" };
        }
    }
}
