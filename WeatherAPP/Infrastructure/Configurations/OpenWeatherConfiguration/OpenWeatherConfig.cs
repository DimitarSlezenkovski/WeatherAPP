using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using WeatherAPP.Infrastructure.Caching;
using WeatherAPP.Infrastructure.ExternalServices;

namespace WeatherAPP.API.Infrastructure.Configurations.OpenWeatherConfiguration
{
    public static class OpenWeatherConfig
    {
        public static IServiceCollection AddOpenWeatherService(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddHttpClient<IWeatherService, OpenWeatherService>(client =>
            {
                client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            var key = configuration.GetSection("OpenWeatherSettings:appid");
            services.Configure<OpenWeatherSettings>(key);

            services.AddTransient<IWeatherService>(opts =>
            {
                return new OpenWeatherService(configuration);
            });
            services.AddTransient<IOpenWeatherMemoryCache, OpenWeatherMemoryCache>();
            return services;
        }
    }
}
