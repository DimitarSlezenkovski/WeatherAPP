using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPP.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToFormattedString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToFormattedString(this TimeSpan timeSpan)
        {
            return timeSpan.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
