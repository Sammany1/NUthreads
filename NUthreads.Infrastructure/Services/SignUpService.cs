using Microsoft.AspNetCore.Mvc;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Services;
using NUthreads.Application.Interfaces.Validators;
using NUthreads.Domain.DTOs;
using NUthreads.Domain.Models;

namespace NUthreads.Infrastructure.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly IUserRepository _users;
        private readonly ISignUpValidator _validator;

        public SignUpService(IUserRepository userRepository, ISignUpValidator validator)
        {
            _users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<IActionResult> SignUp(NewUserDTO newUser)
        {
            var result = _validator.Validate(newUser);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage);
                return new BadRequestObjectResult(errors);
            }

            var user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.UserName,
                Email = newUser.Email,
                Password = newUser.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            if (!await _users.CreateAsync(user))
            {
                return new StatusCodeResult(500); // el howa y3ny fe mo4kla fel database
            }

            return new OkObjectResult("User created successfully");
        }
    }
}
