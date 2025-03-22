using CleanArch.Application.Interfaces.Repositories;
using CleanArch.Domain.Models;
using CleanArch.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Repositories
{
    public class BaseRepository<T>(CleanArchDbContext context) : IBaseRepository<T> where T : BaseEntity
    {
        private readonly CleanArchDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DbSet<T> _table = context.Set<T>();
        public async Task Create(T entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _table.FindAsync(id);
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
