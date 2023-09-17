using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using WeatherAPP.Data.Entities.Weather;

namespace WeatherAPP.Infrastructure.ExternalServices
{
    public class OpenWeatherService : IWeatherService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _openWeatherApiKey;
        private readonly IConfiguration _configuration;

        public OpenWeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
            _openWeatherApiKey = _configuration.GetSection("OpenWeatherSettings").GetValue<string>("appid") ?? "";
            _httpClient = new HttpClient();
        }

        public async Task<WeatherData> GetCurrentWeatherByCityAsync(string city)
        {
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={_openWeatherApiKey}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    WeatherData weatherData = await response.Content.ReadFromJsonAsync<WeatherData>();
                    return weatherData;
                }
                else
                {
                    throw new Exception($"OpenWeather API request failed with status code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while making the API request", ex);
            }
        }

        public async Task<WeatherForecastData> GetWeatherForecastByCityAsync(string city)
        {
            string apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid={_openWeatherApiKey}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var weatherForecast = await response.Content.ReadFromJsonAsync<WeatherForecastData>();
                    return weatherForecast;
                }
                else
                {
                    throw new Exception($"OpenWeather API request failed with status code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while making the API request", ex);
            }
        }
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
