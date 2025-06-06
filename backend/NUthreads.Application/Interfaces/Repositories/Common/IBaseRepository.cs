using NUthreads.Domain.Models;

namespace NUthreads.Application.Interfaces.Repositories.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<bool> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
    }
}
