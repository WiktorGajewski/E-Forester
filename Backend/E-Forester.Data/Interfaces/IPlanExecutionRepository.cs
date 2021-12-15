using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IPlanExecutionRepository
    {
        Task<ICollection<PlanExecution>> GetPlanExecutionsAsync();
        Task CreatePlanExecutionAsync(PlanExecution newPlanExecution);
    }
}
