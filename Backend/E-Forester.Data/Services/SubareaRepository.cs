using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Data.Services
{
    public class SubareaRepository : ISubareaRepository
    {
        private readonly E_ForesterDbContext _context;

        public SubareaRepository(E_ForesterDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Subarea>> GetSubareasAsync()
        {
            return await _context.Subareas.ToListAsync();
        }

        public async Task CreateSubareaAsync(Subarea newSubarea)
        {
            await _context.Subareas.AddAsync(newSubarea);
            await _context.SaveChangesAsync();
        }
    }
}
