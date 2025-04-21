namespace NUthreads.Domain.Models
{
    public class User : BaseEntity
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        // Profile pic somehow
        public User[] Followers { get; set; }
        public User[] Following { get; set; }

        public String[] Blockedids { get; set; }
        public Post[] Posts { get; set; }

        public String[] Favorite_tags { get; set; }

        public string Email { get; set; }

    }
}
