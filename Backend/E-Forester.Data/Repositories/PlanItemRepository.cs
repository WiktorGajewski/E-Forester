using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Repositories
{
    public class PlanItemRepository : IPlanItemRepository
    {
        private readonly E_ForesterDbContext _context;

        public PlanItemRepository(E_ForesterDbContext context)
        {
            _context = context;
        }

        public IQueryable<PlanItem> GetPlanItems()
        {
            return _context.PlanItems.AsQueryable();
        }

        public async Task ClosePlanItemsAsync(List<int> planItemIds)
        {
            var planItems = await _context.PlanItems.Where(planItem => planItemIds.Contains(planItem.Id)).ToListAsync();

            planItems.ForEach(planItem => planItem.IsCompleted = true);
            await _context.SaveChangesAsync();
        }

        public async Task OpenPlanItemsAsync(List<int> planItemIds)
        {
            var planItems = await _context.PlanItems.Where(planItem => planItemIds.Contains(planItem.Id)).ToListAsync();

            planItems.ForEach(planItem => planItem.IsCompleted = false);
            await _context.SaveChangesAsync();
        }

        public async Task AddPlanItemAsync(PlanItem newPlanItem)
        {
            if (newPlanItem == null)
                throw new System.NullReferenceException();

            await _context.PlanItems.AddAsync(newPlanItem);
            await _context.SaveChangesAsync();
        }
    }
}
