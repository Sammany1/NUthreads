using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Domain.Models;

namespace NUthreads.Infrastructure.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> GetAllUsers();

        Task<bool> DeleteAllUsers();
    }
}