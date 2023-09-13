using Microsoft.EntityFrameworkCore;
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
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckIfUserExists(string email);
        Task<User> GetUser(string Email);
    }
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            return await context.Set<User>().Where(user => user.Email == email).AnyAsync();
        }

        public async Task<User> GetUser(string Email)
        {
            return await context.Set<User>().FirstAsync(user => user.Email == Email);
        }
    }
}
