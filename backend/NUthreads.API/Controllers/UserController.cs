using Microsoft.AspNetCore.Mvc;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Services;
using NUthreads.Domain.DTOs;

namespace NUthreads.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ISignUpService _signUpService;

        public UserController(IUserRepository repository, ISignUpService signUpService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _signUpService = signUpService ?? throw new ArgumentNullException(nameof(signUpService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user is not null ? Ok(user) : NotFound("User not found");
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] NewUserDTO userToCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _signUpService.SignUp(userToCreate);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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