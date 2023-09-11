using System.Linq.Expressions;
using WeatherAPP.Data.Entities;
using WeatherAPP.Data;
using Microsoft.EntityFrameworkCore;

namespace WeatherAPP.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly DatabaseContext context;

        public RepositoryBase(DatabaseContext context)
        {
            this.context = context;
        }

        public virtual async Task<IEnumerable<T>> Query(Expression<Func<T, bool>>? @where = null)
        {
            if (@where is null)
            {
                return await context.Set<T>().ToListAsync();
            }

            return await context.Set<T>().Where(@where).ToListAsync();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task Insert(T entity)
        {
            if (entity is not null)
                await context.Set<T>().AddAsync(entity);
        }

        public virtual async Task Insert(IEnumerable<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
        }

        public void Update(IEnumerable<T> entities)
        {
        }

        public void Delete(T entity)
        {
            if (entity is not null)
                context.Set<T>().Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity is not null)
            {
                Delete(entity);
            }
        }

        public virtual async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
