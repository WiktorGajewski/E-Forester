using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IPlanItemRepository
    {
        IQueryable<PlanItem> GetPlanItems();
        Task ClosePlanItemsAsync(List<int> planItemIds);
        Task OpenPlanItemsAsync(List<int> planItemIds);
        Task CreatePlanItemAsync(PlanItem newPlanItem);
    }
}
