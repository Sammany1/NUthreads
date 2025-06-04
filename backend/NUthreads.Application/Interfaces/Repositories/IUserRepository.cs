using NUthreads.Domain.Models;
using NUthreads.Domain.DTOs;
using NUthreads.Application.Interfaces.Repositories.Common;

namespace NUthreads.Application.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<bool> EmailExistsAsync(string email);
        public Task<bool> UsernameExistsAsync(string username);

        public bool EmailExists(string Email);
        public Task<string?> GetPasswordByEmail(string email);
        public bool UsernameExists(string username);

    }
}