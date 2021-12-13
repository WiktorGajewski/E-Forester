using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Data.Services
{
    public class PlanRepository : IPlanRepository
    {
        private readonly E_ForesterDbContext _context;

        public PlanRepository(E_ForesterDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Plan>> GetPlansAsync()
        {
            return await _context.Plans.ToListAsync();
        }

        public async Task CreatePlanAsync(Plan newPlan)
        {
            await _context.Plans.AddAsync(newPlan);
            await _context.SaveChangesAsync();
        }
    }
}
