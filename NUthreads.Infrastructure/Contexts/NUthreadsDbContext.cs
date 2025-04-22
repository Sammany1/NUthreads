
using MongoDB.Driver;
using NUthreads.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace NUthreads.Infrastructure.Contexts
{
    public class NUthreadsDbContext
    {
        private readonly IMongoDatabase _database;

        public NUthreadsDbContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionStrings);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Posts");
        public IMongoCollection<Reply> Replies => _database.GetCollection<Reply>("Replies");
    }
}