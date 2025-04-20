using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NUthreads.Infrastructure.Repositories;
using NUthreads.Domain.DTOs;

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
            var user = await _repository.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] NewUserDTO UserToCreate)
        {
            User Created_User = new User();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Created_User = await _repository.CreateUserAsync(UserToCreate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return CreatedAtAction(nameof(GetUser), new { id = Created_User.Id }, Created_User);
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
            var collection = await _repository.GetAllAsync();
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