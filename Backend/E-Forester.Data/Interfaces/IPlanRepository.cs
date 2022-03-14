using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IPlanRepository
    {
        IQueryable<Plan> GetPlans();
        Task<Plan> GetPlanAsync(int planId);
        Task ClosePlanAsync(Plan plan);
        Task OpenPlanAsync(Plan plan);
        Task AddPlanAsync(Plan newPlan);
    }
}
