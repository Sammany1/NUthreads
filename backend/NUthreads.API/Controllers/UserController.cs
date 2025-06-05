using Microsoft.AspNetCore.Authorization;
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
        private readonly ILoginService _loginService;
        public UserController(IUserRepository repository, ISignUpService signUpService, ILoginService loginService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _signUpService = signUpService ?? throw new ArgumentNullException(nameof(signUpService));
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
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
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO EmailAndPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Call your service method
            return await _loginService.Login(EmailAndPassword.Password, EmailAndPassword.Email.ToLower());
        }

        [Authorize]
        [HttpGet("GetByID")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user is not null ? Ok(user) : NotFound("User not found");
        }
        [Authorize]
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