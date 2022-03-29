using E_Forester.Infrastructure.Database;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Infrastructure.Repositories
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

        public async Task AddPlanExecutionAsync(PlanExecution newPlanExecution)
        {
            if (newPlanExecution == null)
                throw new System.NullReferenceException();

            await _context.PlanExecutions.AddAsync(newPlanExecution);
            await _context.SaveChangesAsync();
        }
    }
}
