using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IDivisionRepository
    {
        Task<ICollection<Division>> GetDivisionsAsync();
        Task CreateDivisionAsync(Division newDivision);
    }
}
