using E_Forester.Infrastructure.Database;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Infrastructure.Repositories
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

        public async Task ClosePlanAsync(Plan plan)
        {
            plan.IsCompleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task OpenPlanAsync(Plan plan)
        {
            plan.IsCompleted = false;
            await _context.SaveChangesAsync();
        }

        public async Task AddPlanAsync(Plan newPlan)
        {
            if (newPlan == null)
                throw new System.NullReferenceException();

            await _context.Plans.AddAsync(newPlan);
            await _context.SaveChangesAsync();
        }
    }
}
