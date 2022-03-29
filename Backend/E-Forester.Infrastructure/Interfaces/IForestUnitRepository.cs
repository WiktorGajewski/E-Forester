using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Infrastructure.Interfaces
{
    public interface IForestUnitRepository
    {
        IQueryable<ForestUnit> GetForestUnits();
        Task<ForestUnit> GetForestUnitAsync(int id);
        Task AddForestUnitAsync(ForestUnit newForestUnit);
    }
}
