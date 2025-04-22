namespace NUthreads.Domain.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public long Version { get; set; }
    }
}