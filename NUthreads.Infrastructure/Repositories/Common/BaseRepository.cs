using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NUthreads.Application.Interfaces.Repositories.Common;
using NUthreads.Domain.Models;
using NUthreads.Infrastructure.Contexts;

namespace NUthreads.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly NUthreadsDbContext _dbcontext;
        private readonly IMongoCollection<T> _entities;

        public BaseRepository(NUthreadsDbContext mongoContext)
        {
            _entities = typeof(T) switch
            {
                var t when t == typeof(User) => mongoContext.Users as IMongoCollection<T>,
                var t when t == typeof(Post) => mongoContext.Posts as IMongoCollection<T>,
                var t when t == typeof(Reply) => mongoContext.Replies as IMongoCollection<T>,
                _ => throw new ArgumentException("Unsupported type")
            } ?? throw new InvalidOperationException("Failed to resolve collection");
        }


        public async Task CreateAsync(T newEntity)
        {
            await _entities.InsertOneAsync(newEntity);
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            var entity = await _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            await _entities.ReplaceOneAsync(filter, entity);
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var result = await _entities.DeleteOneAsync(e => e.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
