using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPP.Infrastructure.Validation;

namespace WeatherAPP.Infrastructure.Services
{
    public interface IServiceBase
    {
    }
    public abstract class ServiceBase<TIn, TOut> : IServiceBase
    {
        protected ServiceContext Context;

        protected ServiceBase(ServiceContext context)
        {
            Context = context;
        }

        public virtual void ValidateInput(InputValidator<TIn> validator, TIn input)
        {
            validator.Validate(input);
        }

        public abstract Task<TOut> Handle(TIn input);
    }
}
