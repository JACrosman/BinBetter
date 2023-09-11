using BinBetter.Api.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace BinBetter.Api.Data
{
    public class BinBetterContext : DbContext
    {
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Bin> Bins { get; set; }

        public BinBetterContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bin>().HasData
            (
                new Goal { Id = 1, Name = "Bin 1", Description = "My first Bin" }
            );

            modelBuilder.Entity<Goal>().HasData
            (
                new Goal { Id = 1, BinId = 1, Name = "Goal 1", Description = "My first goal" },
                new Goal { Id = 2, BinId = 1, Name = "Goal 2", Description = "My second goal" },
                new Goal { Id = 3, BinId = 1, Name = "Goal 3", Description = "My third goal" }
            );
        }
    }
}
