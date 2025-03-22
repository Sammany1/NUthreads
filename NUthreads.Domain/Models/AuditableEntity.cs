namespace NUthreads.Domain.Models
{
    public abstract class AuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
