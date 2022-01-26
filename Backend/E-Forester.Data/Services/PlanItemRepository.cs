using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Services
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

        public async Task CreatePlanItemAsync(PlanItem newPlanItem)
        {
            await _context.PlanItems.AddAsync(newPlanItem);
            await _context.SaveChangesAsync();
        }
    }
}
