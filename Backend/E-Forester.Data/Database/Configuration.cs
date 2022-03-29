using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Forester.Infrastructure.Database
{
    public static class Configuration
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<E_ForesterDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("E_ForesterDb"));
            });

            return services;
        }
    }
}
