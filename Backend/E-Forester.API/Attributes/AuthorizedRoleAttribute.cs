using E_Forester.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace E_Forester.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizedRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly List<UserRole> _roles;

        public AuthorizedRoleAttribute(UserRole[] roles)
        {
            _roles = roles?.ToList() ?? new List<UserRole>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRoleClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            Enum.TryParse(userRoleClaim.Value, out UserRole userRole);


            if (_roles.Any(restrictedRole => userRole.HasFlag(restrictedRole)))
                return;

            context.Result = new ForbidResult();
        }
    }
}
