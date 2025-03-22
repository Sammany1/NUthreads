using CleanArch.Application.Interfaces.Repositories;
using CleanArch.Domain.Models;
using CleanArch.Infrastructure.Contexts;

namespace CleanArch.Infrastructure.Repositories
{
    public class SMUserRepository(CleanArchDbContext context) : BaseRepository<SMUser>(context),ISMUserRepository
    {
        private readonly CleanArchDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task Crasate(SMUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
