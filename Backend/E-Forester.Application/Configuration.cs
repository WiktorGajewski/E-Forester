using E_Forester.Application.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace E_Forester.Application
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureServicesApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSecurityServices(configuration);

            return services;
        }
    }
}
