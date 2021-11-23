using E_Forester.Data.Database;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using System.Collections.Generic;
using System.Linq;

namespace E_Forester.Data.Services
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly E_ForesterDbContext _context;

        public WeatherForecastRepository(E_ForesterDbContext context)
        {
            _context = context;
        }

        public ICollection<WeatherForecast> Get()
        {
            return _context.WeatherForecasts.ToList();
        }

        public void Post(WeatherForecast weatherForecast)
        {
            _context.WeatherForecasts.Add(weatherForecast);
            _context.SaveChanges();
        }
    }
}
