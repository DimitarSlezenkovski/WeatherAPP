using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPP.Infrastructure.Context
{
    public class UserExtensions
    {
        public static string IdentifierClaim => "Id";

        public static string FirstNameClaim => "FirstName";

        public static string LastNameClaim => "LastName";
    }
}
