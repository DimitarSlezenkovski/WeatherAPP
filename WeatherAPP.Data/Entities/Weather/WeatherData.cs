using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPP.Data.Entities.Weather
{
    public class WeatherData
    {
        public ICollection<Weather> Weather { get; set; }
        public Sys Sys { get; set; }
        public Main Main { get; set; }  
        public Wind Wind { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
