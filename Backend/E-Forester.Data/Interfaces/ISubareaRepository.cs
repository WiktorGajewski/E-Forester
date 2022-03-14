using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface ISubareaRepository
    {
        IQueryable<Subarea> GetSubareas();
        Task<Subarea> GetSubareaAsync(int subareaId);
        Task AddSubareaAsync(Subarea newSubarea);
    }
}
