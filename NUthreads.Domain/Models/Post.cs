namespace NUthreads.Domain.Models
{
    public class Post : BaseEntity
    {
        public required string Title { get; set; }
        public required string Text { get; set; }
        public string UserId { get; set; }
    }
}
