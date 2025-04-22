namespace NUthreads.Domain.Models
{
    public abstract class AuditableEntity : BaseEntity
    {
        public required string CreatedBy { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
