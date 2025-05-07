namespace NUthreads.Domain.Models
{
    public class User : AuditableEntity
    {

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        // Profile pic somehow
        public String[]? Followersids { get; set; }
        public String[]? Followingids { get; set; }

        public String[]? Blockedids { get; set; }
        public String[]? Postids { get; set; }

        public String[]? Favorite_tags { get; set; }



    }
}
