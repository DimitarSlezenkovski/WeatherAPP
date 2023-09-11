using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeatherAPP.Data;
using WeatherAPP.Data.Entities.Users;

namespace WeatherAPP.Repository.Users
{
    public interface IUserRepository : IRepository<User> { }
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
