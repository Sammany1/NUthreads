using CleanArch.Application.Interfaces.Repositories;
using CleanArch.Domain.DTOs;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(ISMUserRepository repository) : Controller
    {
        private readonly ISMUserRepository _repository = repository = repository
            ?? throw new ArgumentNullException(nameof(repository));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(NewUserDTO auser)
        {
            SMUser user = new SMUser
            {
                Name = auser.Name,
                Email = auser.Email
            };

            await _repository.Create(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
    }
}
