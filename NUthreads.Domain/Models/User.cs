namespace NUthreads.Domain.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual IList<Reply> Replies { get; set; }
    }
}
