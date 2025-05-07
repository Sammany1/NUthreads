namespace NUthreads.Domain.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public long Version { get; set; }
    }

}