using Microsoft.EntityFrameworkCore;
using NUthreads.Application.Interfaces.Repositories.Common;
using NUthreads.Domain.Models;
using NUthreads.Infrastructure.Contexts;

namespace NUthreads.Infrastructure.Repositories.Common
{
    public class BaseRepository<T>(NUthreadsDbContext context) : IBaseRepository<T> where T : BaseEntity
    {
        private readonly NUthreadsDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task CreateAsync(T newEntity)
        {
            await _dbSet.AddAsync(newEntity);
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
