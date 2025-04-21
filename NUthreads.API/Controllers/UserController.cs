using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NUthreads.Infrastructure.Repositories;
using NUthreads.Domain.DTOs;
using Microsoft.AspNetCore.Authentication;

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

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var collection = await _repository.GetAllAsync();
            return Ok(collection);
        }
        [HttpDelete("DeleteUserByID")]
        public async Task<IActionResult> DeleteUserByID(string Id)
        {
            if (await _repository.DeleteAsync(Id)){
                return Ok("User With ID : " + Id + "Deleted Successfully");
            }
            else
            {
                return BadRequest("FAILED TO DELETE USER WITH ID : " + Id);
            }
        }

    }
}