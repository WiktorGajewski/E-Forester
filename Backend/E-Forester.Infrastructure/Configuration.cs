using E_Forester.Infrastructure.Database;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Forester.Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureServicesData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IForestUnitRepository, ForestUnitRepository>();
            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<ISubareaRepository, SubareaRepository>();
            services.AddScoped<IPlanItemRepository, PlanItemRepository>();
            services.AddScoped<IPlanExecutionRepository, PlanExecutionRepository>();

            return services;
        }
    }
}
