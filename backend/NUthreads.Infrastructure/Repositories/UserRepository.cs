using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Domain.DTOs;
using NUthreads.Domain.Models;
using NUthreads.Infrastructure.Contexts;
using NUthreads.Infrastructure.Repositories.Common;


namespace NUthreads.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly NUthreadsDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(NUthreadsDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _users = context.Users;
        }

        public async Task<bool> EmailExistsAsync(string Email)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Email == Email);
            return user != null;
        }
        public bool EmailExists(string Email)
        {
            var user = _users.FirstOrDefault(x => x.Email == Email);
            return user != null;
        }
        public async Task<string?> GetPasswordByEmail(string email)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Email == email);
            return user.Password; // returns  null if not found
        }
        public async Task<bool> UsernameExistsAsync(string Username)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.UserName == Username);
            return user != null;
        }
        public bool UsernameExists(string Username)
        {
            var user = _users.FirstOrDefault(x => x.UserName == Username);
            return user != null;
        }
    }
}
