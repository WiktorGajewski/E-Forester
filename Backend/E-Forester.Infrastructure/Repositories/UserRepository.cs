using E_Forester.Infrastructure.Database;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace E_Forester.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly E_ForesterDbContext _context;

        public UserRepository(E_ForesterDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Authenticate(string login, string password)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Login == login);

            if (user == null)
                return false;

            if (!BC.Verify(password, user.Password))
                return false;

            return true;
        }

        public IQueryable<User> GetUsers()
        {
            return _context.AppUsers.AsQueryable();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.AppUsers
                .Include(u => u.AssignedForestUnits)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserAsync(string login)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task RegisterUserAsync(User newUser)
        {
            if (newUser == null)
                throw new NullReferenceException();

            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Login == newUser.Login);

            if (user != null)
                throw new ArgumentException("User with given login already exists.");

            newUser.Password = BC.HashPassword(newUser.Password);

            await _context.AppUsers.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task ReactivateUserAsync(User user)
        {
            if (user == null)
                throw new NullReferenceException();

            user.IsActive = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateUserAsync(User user)
        {
            if (user == null)
                throw new NullReferenceException();

            user.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(User user, string newPasword)
        {
            if (user == null)
                throw new NullReferenceException();

            user.Password = BC.HashPassword(newPasword);
            await _context.SaveChangesAsync();
        }

        public async Task AssignForestUnitAsync(User user, ForestUnit forestUnit)
        {
            if (user == null || forestUnit == null)
                throw new NullReferenceException();

            user.AssignedForestUnits.Add(forestUnit);
            forestUnit.AssignedUsers.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UnassignForestUnitAsync(User user, ForestUnit forestUnit)
        {
            if (user == null || forestUnit == null)
                throw new NullReferenceException();

            user.AssignedForestUnits.Remove(forestUnit);
            forestUnit.AssignedUsers.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddRefreshToken(RefreshToken token, User user)
        {
            if (token == null || user == null)
                throw new NullReferenceException();

            user.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByRefreshTokenAsync(string token)
        {
            return await _context.AppUsers
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
        }

        public async Task RevokeRefreshTokenAsync(RefreshToken token)
        {
            if (token == null)
                throw new NullReferenceException();

            token.Revoked = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveExpiredRefreshTokensAsync(User user)
        {
            if (user == null)
                throw new NullReferenceException();

            var expiredTokens = user.RefreshTokens
                .Where(t => t.IsExpired).ToList();

            foreach(var expiredToken in expiredTokens)
            {
                user.RefreshTokens.Remove(expiredToken);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RevokeAllRefreshTokens(User user)
        {
            if (user == null)
                throw new NullReferenceException();

            var refreshTokens = user.RefreshTokens
                .Where(t => t.IsActive).ToList();

            foreach(var refreshToken in refreshTokens)
            {
                refreshToken.Revoked = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}
