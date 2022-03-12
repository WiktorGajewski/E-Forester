using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Repositories
{
    public class SubareaRepository : ISubareaRepository
    {
        private readonly E_ForesterDbContext _context;

        public SubareaRepository(E_ForesterDbContext context)
        {
            _context = context;
        }

        public IQueryable<Subarea> GetSubareas()
        {
            return _context.Subareas.AsQueryable();
        }

        public async Task<Subarea> GetSubareaAsync(int subareaId)
        {
            return await _context.Subareas
                .Include(s => s.Division)
                .FirstOrDefaultAsync(s => s.Id == subareaId);
        }

        public async Task CreateSubareaAsync(Subarea newSubarea)
        {
            await _context.Subareas.AddAsync(newSubarea);
            await _context.SaveChangesAsync();
        }
    }
}
