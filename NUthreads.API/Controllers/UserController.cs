using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NUthreads.Infrastructure.Repositories;

namespace NUthreads.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _repository.GetUserById(id);
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User auser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User
            {
                Name = auser.Name,
                Email = auser.Email
            };

            await _repository.Add_User(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var collection = await _repository.GetAllUsers();
            return Ok(collection);
        }
    }
}