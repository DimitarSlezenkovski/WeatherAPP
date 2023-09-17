using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPP.Data.Entities.Weather;
using WeatherAPP.Infrastructure.Caching;
using WeatherAPP.Infrastructure.Services;

namespace WeatherAPP.Application.Services.OpenWeather
{
    public class GetWeatherForecastRequest
    {
        public string City { get; set; }
        public GetWeatherForecastRequest(string city)
        {
            City = city;    
        }
    }

    public class GetWeatherForecastResponse
    {
        public WeatherForecastData WeatherForecastData { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class GetWeatherForecast : ServiceBase<GetWeatherForecastRequest, GetWeatherForecastResponse>
    {
        private readonly IOpenWeatherMemoryCache openWeatherMemoryCache;
        public GetWeatherForecast(ServiceContext context,
            IOpenWeatherMemoryCache openWeatherMemoryCache) : base(context)
        {
            this.openWeatherMemoryCache = openWeatherMemoryCache;
        }

        public override async Task<GetWeatherForecastResponse> Handle(GetWeatherForecastRequest input)
        {
            var response = await openWeatherMemoryCache.GetWeatherForecastByCity(input.City);

            return new GetWeatherForecastResponse
            {
                WeatherForecastData = response,
                Success = true,
                Message = ""
            };
        }
    }
}
