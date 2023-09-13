using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPP.Infrastructure.Mediating
{
    public abstract class MediatorBase
    {
        private readonly IServiceProvider services;

        public MediatorBase(IServiceProvider services)
        {
            this.services = services;
        }

        protected TOut? GetService<TOut>(bool isRequired)
            where TOut : class
        {
            var service = services.GetService(typeof(TOut)) as TOut;

            if (isRequired && service == null)
            {
                throw new NullReferenceException($"No service {typeof(TOut).Name} registered");
            }

            return service;
        }
    }
}
