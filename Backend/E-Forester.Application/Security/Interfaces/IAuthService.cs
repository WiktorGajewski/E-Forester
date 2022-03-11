using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Forester.Application.Security.Interfaces
{
    public interface IAuthService
    {
        int GetCurrentUserId();
        UserRole GetCurrentUserRole();
        Task<ICollection<ForestUnit>> GetAssignedForestUnits();
    }
}
