using NUthreads.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using NUthreads.Infrastructure.Contexts;
using NUthreads.Domain.Models;
using NUthreads.Application.Interfaces.Repositories;

namespace NUthreads.Infrastructure.Repositories
{
    public class RevokedTokenRepository : BaseRepository<RevokedToken>, IRevokedTokenRepository
    {
        private readonly NUthreadsDbContext _context;
        private readonly DbSet<RevokedToken> _tokens;

        public RevokedTokenRepository(NUthreadsDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _tokens = context.RevokedTokens;
        }

        public async Task AddAsync(RevokedToken token)
        {
            await _tokens.AddAsync(token);
        }

        public async Task<bool> IsTokenRevokedAsync(string token)
        {
            var result = await _tokens.FirstOrDefaultAsync(x => x.Token == token);
            return result != null;
        }
    }
}
