using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WeatherAPP.Infrastructure.Context
{
    public class UserPrincipal : IUserPrincipal
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserPrincipal() {}
        public UserPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            Id = Guid.Parse(claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            FirstName = claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.Name)!.Value;
            LastName = claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.Name)!.Value;
        }
    }
}
