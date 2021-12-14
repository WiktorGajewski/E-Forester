using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IForestUnitRepository
    {
        Task<ICollection<ForestUnit>> GetForestUnitsAsync();
        Task CreateForestUnitAsync(ForestUnit newForestUnit);
    }
}
