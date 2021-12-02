using BC = BCrypt.Net.BCrypt;
using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace E_Forester.Data.Services
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

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserAsync(string login)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task RegisterUserAsync(User newUser)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Login == newUser.Login);

            if (user != null)
                throw new ArgumentException("User with given login already exists.");

            newUser.Password = BC.HashPassword(newUser.Password);

            await _context.AppUsers.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
