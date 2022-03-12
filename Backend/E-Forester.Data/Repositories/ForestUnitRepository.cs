using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Repositories
{
    public class ForestUnitRepository : IForestUnitRepository
    {
        private readonly E_ForesterDbContext _context;

        public ForestUnitRepository(E_ForesterDbContext context)
        {
            _context = context;
        }

        public IQueryable<ForestUnit> GetForestUnits()
        {
            return _context.ForestUnits.AsQueryable();
        }

        public async Task<ForestUnit> GetForestUnitAsync(int id)
        {
            return await _context.ForestUnits
                .Include(f => f.AssignedUsers)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task CreateForestUnitAsync(ForestUnit newForestUnit)
        {
            await _context.ForestUnits.AddAsync(newForestUnit);
            await _context.SaveChangesAsync();
        }
    }
}
