using E_Forester.Infrastructure.Database;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Infrastructure.Repositories
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

        public async Task AddForestUnitAsync(ForestUnit newForestUnit)
        {
            if (newForestUnit == null)
                throw new System.NullReferenceException();

            await _context.ForestUnits.AddAsync(newForestUnit);
            await _context.SaveChangesAsync();
        }
    }
}
