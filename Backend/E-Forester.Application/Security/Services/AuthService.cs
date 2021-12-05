using E_Forester.Application.Security.Interfaces;
using E_Forester.Model.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace E_Forester.Application.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(id);
        }

        public UserRole GetCurrentUserRole()
        {
            var role = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            return (UserRole) int.Parse(role);
        }
    }
}
