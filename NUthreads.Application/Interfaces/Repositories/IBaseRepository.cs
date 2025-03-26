using NUthreads.Domain.Models;

namespace NUthreads.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetById(string id);
        Task Create(T entity);
        Task Update(T entity);
        Task<bool> Delete(string id);
    }
}
