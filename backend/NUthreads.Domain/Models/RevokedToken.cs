namespace NUthreads.Domain.Models;

public class RevokedToken : BaseEntity
{
    public string? Token { get; set; }
    public DateTime ExpiryDate { get; set; }
}