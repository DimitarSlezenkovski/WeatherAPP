using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPP.Data.Entities.Weather;

namespace WeatherAPP.Infrastructure.Caching
{
    public interface IOpenWeatherMemoryCache
    {
        Task<WeatherForecastData> GetWeatherForecastByCity(string city);
    }
}
