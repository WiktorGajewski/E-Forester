using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<ICollection<PlanExecution>> GetPlanExecutionsAsync()
        {
            return await _context.PlanExecutions.ToListAsync();
        }

        public async Task CreatePlanExecutionAsync(PlanExecution newPlanExecution)
        {
            await _context.PlanExecutions.AddAsync(newPlanExecution);
            await _context.SaveChangesAsync();
        }
    }
}
