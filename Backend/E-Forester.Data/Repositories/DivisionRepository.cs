using E_Forester.Infrastructure.Database;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Infrastructure.Repositories
{
    public class DivisionRepository : IDivisionRepository
    {
        private readonly E_ForesterDbContext _context;

        public DivisionRepository(E_ForesterDbContext context)
        {
            _context = context;
        }

        public IQueryable<Division> GetDivisions()
        {
            return _context.Divisions.AsQueryable();
        }

        public async Task AddDivisionAsync(Division newDivision)
        {
            if(newDivision == null)
                throw new System.NullReferenceException();

            await _context.Divisions.AddAsync(newDivision);
            await _context.SaveChangesAsync();
        }
    }
}
