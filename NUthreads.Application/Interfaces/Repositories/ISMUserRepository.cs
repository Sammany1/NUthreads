using CleanArch.Domain.Models;

namespace CleanArch.Application.Interfaces.Repositories
{
    public interface ISMUserRepository : IBaseRepository<SMUser>
    {
        public Task Crasate(SMUser user);
    }
}
