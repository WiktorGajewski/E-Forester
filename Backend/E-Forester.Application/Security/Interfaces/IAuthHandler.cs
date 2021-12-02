using E_Forester.Model.Database;

namespace E_Forester.Application.Security.Interfaces
{
    public interface IAuthHandler
    {
        string GenerateToken(User user);
    }
}
