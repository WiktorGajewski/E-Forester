using E_Forester.Model.Enums;

namespace E_Forester.Application.Security.Interfaces
{
    public interface IAuthService
    {
        int GetCurrentUserId();
        UserRole GetCurrentUserRole();
    }
}
