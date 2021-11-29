using E_Forester.Application.Security.Interfaces;
using E_Forester.Application.Security.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Forester.Application.Security
{
    public static class Configuration
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthHandler, AuthHandler>();

            return services;
        }
    }
}
