using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPP.Data.Entities.Weather;
using WeatherAPP.Infrastructure.Extensions;
using WeatherAPP.Infrastructure.ExternalServices;

namespace WeatherAPP.Infrastructure.Caching
{
    public class OpenWeatherMemoryCache : IOpenWeatherMemoryCache
    {
        private readonly IMemoryCache memoryCache;
        private readonly IWeatherService weatherService;

        public OpenWeatherMemoryCache(IMemoryCache memoryCache, IWeatherService weatherService)
        {
            this.memoryCache = memoryCache;
            this.weatherService = weatherService;
        }

        public async Task<WeatherForecastData> GetWeatherForecastByCity(string city)
        {
            var forecastData = memoryCache.Get<WeatherForecastData>("forecasts");

            if (forecastData is not null)
            {
                var forecast = forecastData.City is not null && forecastData.City.Name == city;
                if (forecast)
                {
                    var fromattedDate = DateTime.Today.ToFormattedString();
                    var day1 = DateTime.Today.AddDays(1).ToFormattedString();
                    var day2 = DateTime.Today.AddDays(2).ToFormattedString();
                    var day3 = DateTime.Today.AddDays(3).ToFormattedString();
                    var forecasts = forecastData.WeatherForecasts.Where(x => x.DateTime.Equals(fromattedDate) 
                    || x.DateTime.Equals(day1)
                    || x.DateTime.Equals(day2)
                    || x.DateTime.Equals(day3))
                        .ToList();

                    forecastData.WeatherForecasts = forecasts;

                    return await Task.FromResult(forecastData);
                }
            }

            var forecastList = await weatherService.GetWeatherForecastByCityAsync(city);

            memoryCache.Set("forecasts", forecastList);

            return forecastList;
        }
    }
}
