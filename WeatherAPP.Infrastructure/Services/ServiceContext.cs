using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPP.Data;
using WeatherAPP.Data.Entities.Users;
using WeatherAPP.Infrastructure.Context;
using WeatherAPP.Infrastructure.Mediating;

namespace WeatherAPP.Infrastructure.Services
{
    public class ServiceContext
    {
        private readonly DatabaseContext context;

        public ServiceContext(IUserPrincipal principal,
            IServiceMediator serviceMediator,
            DatabaseContext context)
        {
            Principal = principal;
            ServiceMediator = serviceMediator;
            this.context = context;
        }

        public IServiceMediator ServiceMediator { get; set; }
        public IUserPrincipal Principal { get; set; }

        public async Task<User?> GetUser()
        {
            if (Principal is not null)
            {
                return await context.Set<User>()
                    .FirstOrDefaultAsync(x => x.Id == Principal.Id);
            }

            return null;
        }
    }
}
