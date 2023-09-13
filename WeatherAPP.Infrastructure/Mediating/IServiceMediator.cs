using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPP.Infrastructure.Mediating
{
    public interface IServiceMediator
    {
        Task<TOut> Do<TIn, TOut>(TIn input);
    }
}
