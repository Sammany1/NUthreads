using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NUthreads.Domain.Models;

namespace NUthreads.Infrastructure.Contexts
{
    public class NUthreadsDbContext
    {
        private readonly IMongoDatabase _database;

        public NUthreadsDbContext(IConfiguration config)
        {
            var client = new MongoClient("mongodb+srv://AhmedGhaith:Password1@maincluster.xiyna.mongodb.net/?retryWrites=true&w=majority&appName=MainCluster");
            _database = client.GetDatabase("NUthreads");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Posts");
        public IMongoCollection<Reply> Replies => _database.GetCollection<Reply>("Replies");
    }
}
