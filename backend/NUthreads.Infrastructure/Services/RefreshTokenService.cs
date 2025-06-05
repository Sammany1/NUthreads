using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Services;

namespace NUthreads.Infrastructure.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUserRepository _users;
        private readonly ITokenGenerator _tokenGenerator;

        public RefreshTokenService(IUserRepository users, ITokenGenerator tokenGenerator)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        }

        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var user = await _users.GetUserByRefreshToken(refreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new ObjectResult("Invalid or expired refresh token.")
                {
                    StatusCode = 401
                };
            }

            var newAccessToken = _tokenGenerator.GenerateJwtToken(user.Email);
            var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _users.UpdateAsync(user);

            return new OkObjectResult(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        public async Task<bool> RevokeRefreshToken(string userEmail)
        {
            var user = await _users.GetUserByEmail(userEmail);
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.MinValue;
            await _users.UpdateAsync(user);
            return true;
        }


    }
}
