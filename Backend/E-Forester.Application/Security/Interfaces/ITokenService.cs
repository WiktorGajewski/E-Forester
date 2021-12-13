using E_Forester.Model.Database;

namespace E_Forester.Application.Security.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        RefreshToken GenerateRefreshToken();
    }
}
