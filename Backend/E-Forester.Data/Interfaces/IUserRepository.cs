using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Authenticate(string login, string password);
        IQueryable<User> GetUsers();
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string login);
        Task RegisterUserAsync(User newUser);
        Task AssignForestUnitAsync(User user, ForestUnit forestUnit);
        Task AddRefreshToken(RefreshToken token, User user);
        Task<User> GetUserByRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(RefreshToken token);
        Task RemoveExpiredRefreshTokensAsync(User user);
        Task RevokeAllRefreshTokens(User user);
    }
}
