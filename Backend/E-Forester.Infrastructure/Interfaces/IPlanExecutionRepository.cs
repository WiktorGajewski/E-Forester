using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Infrastructure.Interfaces
{
    public interface IPlanExecutionRepository
    {
        IQueryable<PlanExecution> GetPlanExecutions();
        Task AddPlanExecutionAsync(PlanExecution newPlanExecution);
    }
}
