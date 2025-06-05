using NUthreads.Domain.Models;
using NUthreads.Domain.DTOs;
using NUthreads.Application.Interfaces.Repositories.Common;

namespace NUthreads.Application.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);

        bool EmailExists(string email);
        bool UsernameExists(string username);

        Task<string?> GetPasswordByEmail(string email);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByRefreshToken(string refreshToken);
    }
}
