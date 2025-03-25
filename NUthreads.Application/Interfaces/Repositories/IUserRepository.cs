using NUthreads.Domain.Models;

namespace NUthreads.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task Add_User(User newUser);
        Task<User> GetUserById(string id);
        Task<List<User>> GetAllUsers();
    }
}