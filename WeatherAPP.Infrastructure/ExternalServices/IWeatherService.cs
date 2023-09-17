using WeatherAPP.Data.Entities.Weather;

namespace WeatherAPP.Infrastructure.ExternalServices
{
    public interface IWeatherService
    {
        Task<WeatherData> GetCurrentWeatherByCityAsync(string city);
        Task<WeatherForecastData> GetWeatherForecastByCityAsync(string city);
    }
}
