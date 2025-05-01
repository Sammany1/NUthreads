using Microsoft.AspNetCore.Mvc;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Domain.DTOs;
using NUthreads.Domain.Models;

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
            User user = new User();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                user = await _repository.CreateUserAsync(UserToCreate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpDelete("DeleteUserByID")]
        public async Task<IActionResult> DeleteUserByID(string Id)
        {
            if (await _repository.DeleteAsync(Id))
            {
                return Ok("User With ID : " + Id + " Deleted Successfully");
            }
            else
            {
                return BadRequest("FAILED TO DELETE USER WITH ID : " + Id);
            }
        }

    }
}