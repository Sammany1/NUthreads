namespace NUthreads.Domain.Models
{
    public class User : AuditableEntity
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        // Profile pic somehow
        public String[] Followersids { get; set; }
        public String[] Followingids { get; set; }

        public String[] Blockedids { get; set; }
        public String[] Postids { get; set; }

        public String[] Favorite_tags { get; set; }

        public string Email { get; set; }

    }
}
