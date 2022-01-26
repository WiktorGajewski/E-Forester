using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Services
{
    public class PlanExecutionRepository : IPlanExecutionRepository
    {
        private readonly E_ForesterDbContext _context;

        public PlanExecutionRepository(E_ForesterDbContext context)
        {
            _context = context;
        }

        public IQueryable<PlanExecution> GetPlanExecutions()
        {
            return _context.PlanExecutions.AsQueryable();
        }

        public async Task CreatePlanExecutionAsync(PlanExecution newPlanExecution)
        {
            await _context.PlanExecutions.AddAsync(newPlanExecution);
            await _context.SaveChangesAsync();
        }
    }
}
