using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IPlanItemRepository
    {
        Task<ICollection<PlanItem>> GetPlanItemsAsync();
        Task CreatePlanItemAsync(PlanItem newPlanItem);
    }
}
