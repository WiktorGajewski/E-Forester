using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Services
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

        public async Task CreateForestUnitAsync(ForestUnit newForestUnit)
        {
            await _context.ForestUnits.AddAsync(newForestUnit);
            await _context.SaveChangesAsync();
        }
    }
}
