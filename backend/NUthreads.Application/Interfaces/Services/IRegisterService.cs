using Microsoft.AspNetCore.Mvc;
using NUthreads.Domain.DTOs;

namespace NUthreads.Application.Interfaces.Services
{
    public interface IRegisterService
    {
        Task<IActionResult> Register(NewUserDTO newUser);
    }
}
