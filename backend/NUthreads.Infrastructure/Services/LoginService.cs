using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Services;
using NUthreads.Application.Interfaces.Utilities;
using NUthreads.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace NUthreads.Infrastructure.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _users;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher _passwordHasher;
    public LoginService(IUserRepository userRepository, IConfiguration configuration, IPasswordHasher passwordHasher)
    {
        _configuration = configuration;
        _users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }
    public async Task<IActionResult> Login(string password, string email)
    {
        string? storedHashedPassword = await _users.GetPasswordByEmail(email);
        if (storedHashedPassword == null)
        {
            return new NotFoundObjectResult("No Account Exists With This Email.");
        }

        bool isValid = _passwordHasher.VerifyPassword(password, storedHashedPassword);
        if (!isValid)
        {
            return new ObjectResult("Wrong Password.") { StatusCode = 401 };
        }
        var token = GenerateJwtToken(email);  

        return new OkObjectResult(new { Token = token, Message = "Logged In Successfully" });

    }
    public string GenerateJwtToken(string userEmail)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userEmail),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["Token_Duration"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}