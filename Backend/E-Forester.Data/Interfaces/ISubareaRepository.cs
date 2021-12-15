using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface ISubareaRepository
    {
        Task<ICollection<Subarea>> GetSubareasAsync();
        Task CreateSubareaAsync(Subarea newSubarea);
    }
}
