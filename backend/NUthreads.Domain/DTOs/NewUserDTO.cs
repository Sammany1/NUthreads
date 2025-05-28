using System.ComponentModel.DataAnnotations;

namespace NUthreads.Domain.DTOs
{
    public class NewUserDTO
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }

        public required string Email { get; set; }

    }
}
