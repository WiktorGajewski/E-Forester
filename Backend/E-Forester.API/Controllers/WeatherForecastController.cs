using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_Forester.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        private readonly IConfiguration _config;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IWeatherForecastRepository weatherForecastRepository,
            IConfiguration config)
        {
            _logger = logger;
            _weatherForecastRepository = weatherForecastRepository;
            _config = config;
        }

        [HttpGet("random")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("test")]
        public string GetTest()
        {
            return _config.GetValue<string>("Test");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetFormDatabase()
        {
            return _weatherForecastRepository.Get();
        }

        [HttpPost]
        public IActionResult AddToDatabase()
        {
            var rng = new Random();

            var newWeatherForecast = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };

            _weatherForecastRepository.Post(newWeatherForecast);

            return NoContent();
        }
    }
}
