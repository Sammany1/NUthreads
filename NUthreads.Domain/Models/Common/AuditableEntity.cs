namespace NUthreads.Domain.Models
{
    public abstract class AuditableEntity : BaseEntity
    {
        public required string CreatedBy { get; set; }
        public required string UpdatedBy { get; set; }
    }
}
