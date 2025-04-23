using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NUthreads.Domain.Models;
using NUthreads.Infrastructure.Contexts;


namespace NUthreads.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _posts;

        public PostRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("NUthreadsDB");
            _posts = database.GetCollection<Post>("Posts");
        }
        public async Task Create(Post post)
        {
            await _posts.InsertOneAsync(post);
            return;
        }

        public async Task<Post> GetById(string id)
        {
            var post = await _posts.FindAsync(id);
            return post.FirstOrDefault();
        }

        public async Task Update(Post post)
        {
            var filter = Builders<Post>.Filter.Eq(p => p.Id, post.Id);
            await _posts.ReplaceOneAsync(filter, post);
            return;
        }
        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Post>.Filter.Eq(p => p.Id, id);
            var result = await _posts.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}