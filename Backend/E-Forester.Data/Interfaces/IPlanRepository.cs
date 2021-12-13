using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IPlanRepository
    {
        Task<ICollection<Plan>> GetPlansAsync();
        Task CreatePlanAsync(Plan newPlan);
    }
}
