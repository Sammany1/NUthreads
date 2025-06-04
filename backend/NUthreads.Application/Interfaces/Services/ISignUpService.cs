using Microsoft.AspNetCore.Mvc;
using NUthreads.Domain.DTOs;

namespace NUthreads.Application.Interfaces.Services
{
    public interface ISignUpService
    {
        Task<IActionResult> SignUp(NewUserDTO newUser);
    }
}
