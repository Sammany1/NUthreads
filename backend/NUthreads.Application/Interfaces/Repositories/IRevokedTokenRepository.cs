using NUthreads.Application.Interfaces.Repositories.Common;
using NUthreads.Domain.Models;
namespace NUthreads.Application.Interfaces.Repositories;

public interface IRevokedTokenRepository : IBaseRepository<RevokedToken>
{
    Task<bool> IsTokenRevokedAsync(string token);
}
