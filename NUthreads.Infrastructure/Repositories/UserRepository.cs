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
            _context = context;
             _users= context.Users;
        }


        public async Task<User> CreateUserAsync(NewUserDTO NewUser)
        {

            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                UserName = NewUser.UserName,
                Email = NewUser.Email,
                Password = NewUser.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await base.CreateAsync(user);
            return user;
        }

        public async Task<bool> EmailExistsAsync(string Email)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Email == Email);
            return user != null;
        }

        public async Task<bool> UsernameExistsAsync(string Username)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.UserName == Username);
            return user != null;
        }
    }
}
