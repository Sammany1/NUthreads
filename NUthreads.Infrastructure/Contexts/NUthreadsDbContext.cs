using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Ensure this is included
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using NUthreads.Domain.Models;

namespace NUthreads.Infrastructure.Contexts
{
    public class NUthreadsMongoDbContext : DbContext
    {
        

        public NUthreadsMongoDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToCollection("users");
        }
        public DbSet<User> Users { get; init; }
        public DbSet<User> Post { get; init; }
        public DbSet<User> Reply { get; init; }
    }
}
