using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Services;
using NUthreads.Domain.DTOs;
using NUthreads.Domain.Models;
using System.IdentityModel.Tokens.Jwt;

namespace NUthreads.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IRegisterService _RegisterService;
        private readonly ILoginService _loginService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRevokedTokenRepository _revokedTokenRepository;

        public UserController(
            IUserRepository repository,
            IRegisterService RegisterService,
            ILoginService loginService,
            IRefreshTokenService refreshTokenService,
            IRevokedTokenRepository revokedTokenRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _RegisterService = RegisterService ?? throw new ArgumentNullException(nameof(RegisterService));
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            _refreshTokenService = refreshTokenService ?? throw new ArgumentNullException(nameof(refreshTokenService));
            _revokedTokenRepository = revokedTokenRepository ?? throw new ArgumentNullException(nameof(revokedTokenRepository));
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] NewUserDTO userToCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _RegisterService.Register(userToCreate);
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

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO refreshRequest)
        {
            if (string.IsNullOrEmpty(refreshRequest.RefreshToken))
                return BadRequest("Refresh token is required.");

            return await _refreshTokenService.RefreshToken(refreshRequest.RefreshToken);
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
                return BadRequest("No token provided");

            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var expiry = jwtToken.ValidTo;

            var revokedToken = new RevokedToken
            {
                Token = token,
                ExpiryDate = expiry
            };
            await _revokedTokenRepository.CreateAsync(revokedToken);

            var email = User.FindFirst("email")?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized("Invalid token: no email claim found");

            var user = await _repository.GetUserByEmail(email);
            if (user != null)
            {
                user.RefreshToken = null;
                await _repository.UpdateAsync(user);
            }
            else
            {
                return BadRequest("No User Found With That Email.");
            }

            return Ok("Logged out successfully");
        }
        [Authorize]
        [HttpGet("TestAuth")]
        public IActionResult TestAuthorization()
        {
            var email = User.FindFirst("email")?.Value ?? "No email claim";
            return Ok($"Authorized! Email claim: {email}");
        }

    }
}