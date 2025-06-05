using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Services;
using NUthreads.Application.Interfaces.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace NUthreads.Infrastructure.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _users;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;
    public LoginService(IUserRepository userRepository, IConfiguration configuration, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
    {
        _configuration = configuration;
        _users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator)); ;
    }
    public async Task<IActionResult> Login(string password, string email)
    {
        var user = await _users.GetUserByEmail(email);
        if (user == null)
            return new NotFoundObjectResult("No Account Exists With This Email.");
        string storedHashedPassword = user.Password;
        bool isValid = _passwordHasher.VerifyPassword(password, storedHashedPassword);
        if (!isValid)
        {
            return new ObjectResult("Wrong Password.") { StatusCode = 401 };
        }
        var accessToken = _tokenGenerator.GenerateJwtToken(email);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _users.UpdateAsync(user);

        return new OkObjectResult(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Message = "Logged In Successfully"
        });

    }
    


}