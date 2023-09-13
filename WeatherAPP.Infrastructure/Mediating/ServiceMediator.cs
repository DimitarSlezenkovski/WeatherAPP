using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPP.Infrastructure.Services;
using WeatherAPP.Infrastructure.Validation;

namespace WeatherAPP.Infrastructure.Mediating
{
    public class ServiceMediator : MediatorBase, IServiceMediator
    {
        public ServiceMediator(IServiceProvider services) : base(services) { }

        public Task<TOut> Do<TIn, TOut>(TIn input)
        {
            var service = GetService<ServiceBase<TIn, TOut>>(true)!;
            var inputValidator = GetService<InputValidator<TIn>>(false)!;

            if (inputValidator != null)
                service.ValidateInput(inputValidator, input);

            return service.Handle(input);
        }
    }
}
