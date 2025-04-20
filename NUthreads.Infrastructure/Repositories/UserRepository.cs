using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NUthreads.Domain.DTOs;
using NUthreads.Domain.Models;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Application.Interfaces.Validators;
using NUthreads.Application.Interfaces.Repositories.Common;
using FluentValidation;
using System.Security;


namespace NUthreads.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly INewUserDTOValidator _newUserValidator;
        private readonly IUserRepository _userRepository;

        public UserRepository(INewUserDTOValidator newUserValidator,IMongoClient mongoClient) : base(mongoClient)
        {
            var database = mongoClient.GetDatabase("NUthreadsDB");
            _newUserValidator = newUserValidator;
            _users = database.GetCollection<User>("Users");
        }


        public async Task<User> CreateUserAsync(NewUserDTO NewUser)
        {
            if (await this.EmailExistsAsync(NewUser.Email))
            {
                throw new Exception("Email Already Exists");
            }
            else if (await this.UsernameExistsAsync(NewUser.UserName))
            {
                throw new Exception("Username Already Exists");

            }
                try
                {
                    var validationResult = await _newUserValidator.ValidateAsync(NewUser);
                    if (!validationResult.IsValid)
                    {

                        throw new ValidationException("Validation failed: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
           
            
            User user = new User
            {
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                UserName = NewUser.UserName,
                Email = NewUser.Email,
                Password = NewUser.Password,
                CreatedAt = DateTime.UtcNow
            };

            await base.CreateAsync(user);
            return user;
        }

        public async Task<bool> EmailExistsAsync(string Email)
        {
            var user = await _users.Find(x => x.Email == Email).FirstOrDefaultAsync();
            return user != null;
        }

        public async Task<bool> UsernameExistsAsync(string Username)
        {
            var user = await _users.Find(x => x.UserName == Username).FirstOrDefaultAsync();
            return user != null;
        }
    }
}
