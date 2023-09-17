using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherAPP.Data.Entities.Weather
{
    public class WeatherForecast
    {
        [JsonPropertyName("dt_txt")]
        public string DateTime { get; set; }
        public Main Main { get; set; }
        public ICollection<Weather> Weather { get; set; }
        public Wind Wind { get; set; }
    }
}
