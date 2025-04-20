using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Domain.Models;
using NUthreads.Domain.DTOs;
using NUthreads.Application.Interfaces.Repositories.Common;

namespace NUthreads.Application.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> CreateUserAsync(NewUserDTO user);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
    }
}