using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public IQueryable<Plan> GetPlans()
        {
            return _context.Plans.AsQueryable();
        }

        public async Task<Plan> GetPlanAsync(int planId)
        {
            return await _context.Plans
                .Include(p => p.ForestUnit)
                .Include(p => p.PlanExecutions)
                .Include(p => p.PlanItems)
                .FirstOrDefaultAsync(p => p.Id == planId);
        }

        public async Task CreatePlanAsync(Plan newPlan)
        {
            await _context.Plans.AddAsync(newPlan);
            await _context.SaveChangesAsync();
        }
    }
}
