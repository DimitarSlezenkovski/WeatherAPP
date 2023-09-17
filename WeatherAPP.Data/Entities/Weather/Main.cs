using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherAPP.Data.Entities.Weather
{
    public class Main
    {
        public decimal Temp { get; set; }
        [JsonPropertyName("feels_like")]
        public decimal FeelsLike { get; set; }
        [JsonPropertyName("temp_min")]
        public decimal TempMin { get; set; }
        [JsonPropertyName("temp_max")]
        public decimal TempMax { get; set; }
        public int Humidity { get; set; }
    }
}
