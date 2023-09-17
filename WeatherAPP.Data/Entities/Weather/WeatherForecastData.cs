using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherAPP.Data.Entities.Weather
{
    public class WeatherForecastData
    {
        public City City { get; set; }
        [JsonPropertyName("list")]
        public ICollection<WeatherForecast> WeatherForecasts { get; set; }
    }
}
