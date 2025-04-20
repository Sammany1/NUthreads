using MongoDB.Driver;
using NUthreads.Domain.Models;
using Microsoft.Extensions.Options;

namespace NUthreads.Infrastructure.Contexts
{
    public class NUthreadsMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public NUthreadsMongoDbContext(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("posts");
        public IMongoCollection<Reply> Replies => _database.GetCollection<Reply>("replies");
    }
}