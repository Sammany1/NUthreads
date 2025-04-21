using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Domain.DTOs;
using NUthreads.Domain.Models;


namespace NUthreads.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly IUserRepository _userRepository;

        public UserRepository(IMongoClient mongoClient) : base(mongoClient)
        {
            var database = mongoClient.GetDatabase("NUthreadsDB");
            _users = database.GetCollection<User>("Users");
        }


        public async Task<User> CreateUserAsync(NewUserDTO NewUser)
        {

            User user = new User
            {
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                UserName = NewUser.UserName,
                Email = NewUser.Email,
                Password = NewUser.Password,
                CreatedAt = DateTime.UtcNow
            };

            await base.CreateAsync(user);
            return user;
        }

        public async Task<bool> EmailExistsAsync(string Email)
        {
            var user = await _users.Find(x => x.Email == Email).FirstOrDefaultAsync();
            return user != null;
        }

        public async Task<bool> UsernameExistsAsync(string Username)
        {
            var user = await _users.Find(x => x.UserName == Username).FirstOrDefaultAsync();
            return user != null;
        }
    }
}
