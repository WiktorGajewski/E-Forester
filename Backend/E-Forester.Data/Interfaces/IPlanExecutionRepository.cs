using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IPlanExecutionRepository
    {
        IQueryable<PlanExecution> GetPlanExecutions();
        Task CreatePlanExecutionAsync(PlanExecution newPlanExecution);
    }
}
