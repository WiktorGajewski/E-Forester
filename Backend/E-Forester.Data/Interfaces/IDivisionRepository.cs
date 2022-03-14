using E_Forester.Model.Database;
using System.Linq;
using System.Threading.Tasks;

namespace E_Forester.Data.Interfaces
{
    public interface IDivisionRepository
    {
        IQueryable<Division> GetDivisions();
        Task AddDivisionAsync(Division newDivision);
    }
}
