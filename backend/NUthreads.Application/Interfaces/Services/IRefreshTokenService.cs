using Microsoft.AspNetCore.Mvc;

namespace NUthreads.Application.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        Task<IActionResult> RefreshToken(string refreshToken);
        Task<bool> RevokeRefreshToken(string userEmail);
    }
}
