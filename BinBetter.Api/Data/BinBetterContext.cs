using BinBetter.Api.Data.Domain;
using BinBetter.Api.Security;
using Microsoft.EntityFrameworkCore;

namespace BinBetter.Api.Data
{
    public class BinBetterContext : DbContext
    {
        private readonly IPasswordHasher _passwordHasher;

        public DbSet<User> Users { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Bin> Bins { get; set; }

        public BinBetterContext(DbContextOptions options, IPasswordHasher passwordHasher)
            : base(options)
        {
            _passwordHasher = passwordHasher;
        }

        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var salt = Guid.NewGuid().ToByteArray();
            var hash = await _passwordHasher.Hash("test", salt);

            modelBuilder.Entity<User>().HasData
            (
                new User
                {
                    UserId = 1,
                    Username = "Fenki",
                    Email = "Fenki@fenki.com",
                    Hash = hash,
                    Salt = salt
                }
            );

            modelBuilder.Entity<Bin>().HasData
            (
                new Bin { BinId = 1, UserId = 1, Name = "Bin 1", Description = "My first Bin" }
            );

            modelBuilder.Entity<Goal>().HasData
            (
                new Goal { GoalId = 1, BinId = 1, Name = "Goal 1", Description = "My first goal" },
                new Goal { GoalId = 2, BinId = 1, Name = "Goal 2", Description = "My second goal" },
                new Goal { GoalId = 3, BinId = 1, Name = "Goal 3", Description = "My third goal" }
            );
        }
    }
}
