using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;

namespace E_Forester.Data.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly E_ForesterDbContext _context;

        public UserRepository(E_ForesterDbContext context)
        {
            _context = context;
        }
    }
}
