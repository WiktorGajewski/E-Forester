﻿using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Forester.Data
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureServicesData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);

            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
