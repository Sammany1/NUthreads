using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using NUthreads.Domain.Models;

namespace NUthreads.Infrastructure.Contexts
{
    public class NUthreadsDbContext(DbContextOptions<NUthreadsDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToCollection("Users");
            modelBuilder.Entity<Post>().ToCollection("Posts");
            modelBuilder.Entity<Reply>().ToCollection("Replies");
        }
        public DbSet<User> Users { get; init; }
        public DbSet<User> Post { get; init; }
        public DbSet<User> Reply { get; init; }
    }
}
