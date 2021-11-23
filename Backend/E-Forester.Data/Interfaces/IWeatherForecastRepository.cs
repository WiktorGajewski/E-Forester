using E_Forester.Model.Database;
using System.Collections.Generic;

namespace E_Forester.Data.Interfaces
{
    public interface IWeatherForecastRepository
    {
        ICollection<WeatherForecast> Get();
        void Post(WeatherForecast weatherForecast);
    }
}
