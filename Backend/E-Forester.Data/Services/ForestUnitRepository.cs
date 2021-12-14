using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<ICollection<ForestUnit>> GetForestUnitsAsync()
        {
            return await _context.ForestUnits.ToListAsync();
        }

        public async Task CreateForestUnitAsync(ForestUnit newForestUnit)
        {
            await _context.ForestUnits.AddAsync(newForestUnit);
            await _context.SaveChangesAsync();
        }
    }
}
