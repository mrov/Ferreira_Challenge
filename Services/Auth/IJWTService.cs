using Models;

namespace Services.Auth
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
