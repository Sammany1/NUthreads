using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NUthreads.Domain.Models;
using NUthreads.Infrastructure.Contexts;


namespace NUthreads.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("NUthreadsDB");
            _users = database.GetCollection<User>("Users");
        }


        public async Task Add_User(User NewUser)
        {
            await _users.InsertOneAsync(NewUser);
            
            return;
        }
        public async Task<User> GetUserById(string id)
        {
            var user = _users.Find(x => x.Id == id).FirstOrDefaultAsync();
            return await user;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _users.Find(_ => true).ToListAsync();
        }



    }
}
