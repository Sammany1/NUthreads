using NUthreads.Application.Interfaces.Repositories.Common;

public interface IRevokedTokenRepository : IBaseRepository<RevokedToken>
{
    Task<bool> IsTokenRevokedAsync(string token);
}
