using Microsoft.AspNetCore.Mvc;
using NUthreads.Domain.DTOs;

namespace NUthreads.Application.Interfaces.Services
{
    public interface ILoginService
    {
        Task<IActionResult> Login(string Email, string Password);
    }
}
