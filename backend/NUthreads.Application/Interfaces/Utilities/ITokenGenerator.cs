namespace NUthreads.Application.Interfaces.Services
{
    public interface ITokenGenerator
    {
        string GenerateJwtToken(string userEmail);
        string GenerateRefreshToken();
    }
}
