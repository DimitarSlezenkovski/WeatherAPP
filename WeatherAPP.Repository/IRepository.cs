using System.Linq.Expressions;
using WeatherAPP.Data.Entities;

namespace WeatherAPP.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> Query(Expression<Func<T, bool>>? @where = null);

        Task<T?> GetById(int id);

        Task Insert(T entity);

        Task Insert(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        Task Delete(int id);

        void Delete(IEnumerable<T> entities);

        Task SaveChangesAsync();
    }
}
