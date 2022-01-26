using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IPlanItemRepository
    {
        IQueryable<PlanItem> GetPlanItems();
        Task CreatePlanItemAsync(PlanItem newPlanItem);
    }
}
