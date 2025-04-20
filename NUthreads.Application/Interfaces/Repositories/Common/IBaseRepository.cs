using NUthreads.Domain.Models;

namespace NUthreads.Application.Interfaces.Repositories.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(string id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task<List<T>> GetAllAsync();
    }
}
