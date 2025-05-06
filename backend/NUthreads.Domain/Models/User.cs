namespace NUthreads.Domain.Models
{
    public class User : BaseEntity
    {

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        // Hal Da S7 ???

        public IList<User>? Followers { get; set; } = new List<User>();
        public IList<User>? Following { get; set; } = new List<User>();

        public IList<Post>? Posts { get; set; } = new List<Post>();


    }
}
