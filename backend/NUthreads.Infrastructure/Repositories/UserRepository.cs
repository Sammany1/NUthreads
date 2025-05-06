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


        public async Task Create(User NewUser)
        {
            await _users.InsertOneAsync(NewUser);

            return;
        }
        public async Task<User> GetById(string id)
        {
            var user = _users.Find(x => x.Id == id).FirstOrDefaultAsync();
            return await user;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _users.Find(_ => true).ToListAsync();
        }
        public async Task<bool> Delete(string id)
        {
            var result = await _users.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }
        //work in progress
        public async Task Update(User user)
        {
            var Old_User = await GetById(user.Id);
            if (Old_User == null)
            {
                throw new Exception("User not found");
            }
            Old_User.Username = user.Username;
            Old_User.Password = user.Password;
            Old_User.FirstName = user.FirstName;
            Old_User.LastName = user.LastName;
            Old_User.Name = user.Name;
            Old_User.Email = user.Email;
            Old_User.Followers = user.Followers;
            Old_User.Following = user.Following;
            Old_User.Posts = user.Posts;
            Old_User.UpdatedAt = DateTime.UtcNow;
        }

        public async Task<bool> DeleteAllUsers()
        {
            if (await _users.CountDocumentsAsync(_ => true) == 0)
            {
                return false;
            }

            await _users.DeleteManyAsync(_ => true);
            return true;
        }
    }
}
