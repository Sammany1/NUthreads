namespace NUthreads.Domain.Models
{
    public class User : AuditableEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public List<string>? Followersids { get; set; }
        public List<string>? Followingids { get; set; }
        public List<string>? Blockedids { get; set; }
        public List<string>? Postids { get; set; }
        public List<string>? Favorite_tags { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
