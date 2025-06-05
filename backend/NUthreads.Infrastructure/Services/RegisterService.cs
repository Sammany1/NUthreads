using Microsoft.AspNetCore.Mvc;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Utilities;
using NUthreads.Application.Interfaces.Validators;
using NUthreads.Application.Interfaces.Services;
using NUthreads.Domain.DTOs;
using NUthreads.Domain.Models;

namespace NUthreads.Infrastructure.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _users;
        private readonly IRegisterValidator _validator;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterService(IUserRepository userRepository, IRegisterValidator validator, IPasswordHasher passwordHasher)
        {
            _users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }
        public async Task<IActionResult> Register(NewUserDTO newUser)
        {
            var result = _validator.Validate(newUser);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage);
                return new BadRequestObjectResult(errors);
            }
            string Hashed_Password = _passwordHasher.HashPassword(newUser.Password);
            var user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.UserName,
                Email = newUser.Email.ToLower(),
                Password = Hashed_Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            if (!await _users.CreateAsync(user))
            {
                return new StatusCodeResult(500); // el howa y3ny fe mo4kla fel database
            }
            return new OkObjectResult("User Registered Successfully");
        }
    }
}
