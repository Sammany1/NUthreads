namespace NUthreads.Domain.Models;

public class Reply : BaseEntity
{
        public string Text { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }

}
