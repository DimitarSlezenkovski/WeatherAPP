using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPP.Infrastructure.Context
{
    public interface IUserPrincipal
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
