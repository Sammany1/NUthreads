using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NUthreads.Application.Interfaces.Repositories.Common;
using NUthreads.Domain.Models;
using NUthreads.Infrastructure.Contexts;

namespace NUthreads.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _entities;


        public BaseRepository(IMongoCollection<T> collection)
        {
            _entities = collection;
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
        virtual public async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            await _entities.ReplaceOneAsync(filter, entity);
        }
        virtual public async Task<bool> DeleteAsync(string id)
        {
            var result = await _entities.DeleteOneAsync(e => e.Id == id);
            if (result.DeletedCount < 1) 
            {
                return false;
            }
            return true;
        }


    }
}
