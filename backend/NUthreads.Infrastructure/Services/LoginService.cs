using Microsoft.AspNetCore.Mvc;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Services;
using NUthreads.Application.Interfaces.Utilities;
using NUthreads.Application.Interfaces.Validators;
namespace NUthreads.Infrastructure.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _users;
    private readonly ISignUpValidator _validator;
    private readonly IPasswordHasher _passwordHasher;
    public LoginService(IUserRepository userRepository, ISignUpValidator validator, IPasswordHasher passwordHasher)
    {
        _users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }
    public async Task<IActionResult> Login(string password, string email)
    {
        string? storedHashedPassword = await _users.GetPasswordByEmail(email);
        if (storedHashedPassword == null)
        {
            return new NotFoundObjectResult("No Account Exists With This Email.") ;
        }

        bool isValid = _passwordHasher.VerifyPassword(password, storedHashedPassword);
        if (!isValid)
        {
            return new ObjectResult("Wrong Password.") { StatusCode = 401 };
        }

        return new OkObjectResult("LoggedIn Successfully.");
    }

}