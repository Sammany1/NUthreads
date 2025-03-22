
using Microsoft.EntityFrameworkCore;

using NUthreads.Domain.Models;

namespace NUthreads.Infrastructure.Contexts
{
    public class NUthreadsDbContext : DbContext
    {
        public NUthreadsDbContext(DbContextOptions<NUthreadsDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Post>();
            modelBuilder.Entity<Reply>();
        }
    }
}