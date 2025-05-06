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

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            // Save to the repository
            await _repository.Create(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _repository.GetById(id);
            return Ok(user);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var collection = await _repository.GetAllUsers();
            return Ok(collection);
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _repository.GetById(user.Id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            await _repository.Update(user);
            return Ok("User updated successfully.");
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _repository.Delete(id);
            if (result)
            {
                return Ok("User deleted successfully.");
            }
            return NotFound("User not found.");
        }
        [HttpDelete("DeleteAllUsers")]
        public async Task<IActionResult> DeleteAllUsers()
        {
            var result = await _repository.DeleteAllUsers();
            if (result)
            {
                return Ok("All users deleted successfully.");
            }
            return NotFound("No users found.");
        }
    }

}