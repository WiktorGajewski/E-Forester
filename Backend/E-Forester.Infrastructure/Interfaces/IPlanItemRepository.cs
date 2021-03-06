using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Infrastructure.Interfaces
{
    public interface IPlanItemRepository
    {
        IQueryable<PlanItem> GetPlanItems();
        Task ClosePlanItemsAsync(List<int> planItemIds);
        Task OpenPlanItemsAsync(List<int> planItemIds);
        Task AddPlanItemAsync(PlanItem newPlanItem);
    }
}
