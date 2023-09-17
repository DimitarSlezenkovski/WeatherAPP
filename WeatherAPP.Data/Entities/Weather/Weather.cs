using System.Text.Json.Serialization;

namespace WeatherAPP.Data.Entities.Weather
{
    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
