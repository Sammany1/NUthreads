using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NUthreads.Domain.Models;
using NUthreads.Infrastructure.Contexts;
using NUthreads.Application.Interfaces.Repositories.Common;

namespace NUthreads.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _entities;

        public BaseRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("NUthreadsDB");
            var collectionName = typeof(T).Name + "s";
            _entities = database.GetCollection<T>(collectionName);
        }
        public async Task CreateAsync(T NewEntity)
        {
            await _entities.InsertOneAsync(NewEntity);

            return;
        }
        virtual public async Task<T> GetByIdAsync(string id)
        {
            var entity = _entities.Find(x => x.Id == id).FirstOrDefaultAsync();
            return await entity;
        }
        virtual public async Task<List<T>> GetAllAsync()
        {
            return await _entities.Find(_ => true).ToListAsync();
        }
        virtual public async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            await _entities.ReplaceOneAsync(filter, entity);
        }
        virtual public async Task DeleteAsync(string id)
        {
            var result = await _entities.DeleteOneAsync(e => e.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("Entity not found");
            }
        }


    }
}
