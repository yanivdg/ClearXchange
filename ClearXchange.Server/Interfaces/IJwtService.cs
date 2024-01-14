using System.Security.Claims;

namespace ClearXchange.Server.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
        ClaimsPrincipal ValidateToken(string token);
    }
}
