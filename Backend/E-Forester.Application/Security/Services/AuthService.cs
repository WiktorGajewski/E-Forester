using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Forester.Application.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public AuthService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public int GetCurrentUserId()
        {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(id);
        }

        public UserRole GetCurrentUserRole()
        {
            var role = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            return (UserRole) Enum.Parse(typeof(UserRole), role);
        }

        public async Task<ICollection<ForestUnit>> GetAssignedForestUnits()
        {
            var id = GetCurrentUserId();
            var user = await _userRepository.GetUserAsync(id);
            return user.AssignedForestUnits;
        }
    }
}
