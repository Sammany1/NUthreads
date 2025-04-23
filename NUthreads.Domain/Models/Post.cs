using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NUthreads.Domain.Models
{
    public class Post : BaseEntity
    {
        
        public string Descreption { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<string> upvotes { get; set; }
        public List<string> downvotes { get; set; }
        public List<Reply> Replies { get; set; } = new List<Reply>();
        public List<string> Tags { get; set; } = new List<string>();

    }
}
