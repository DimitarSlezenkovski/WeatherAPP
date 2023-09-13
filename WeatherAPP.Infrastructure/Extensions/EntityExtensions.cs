using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPP.Data.Entities;

namespace WeatherAPP.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static T ThrowIfNotFound<T>(this T? obj) where T : IEntity
        {
            if (obj is null)
            {
                throw new NullReferenceException($"Entity of type {typeof(T).Name} was not found");
            }

            return obj;
        }

        public static T ThrowIfNotFound<T>(this T? obj, Guid id) where T : IEntity
        {
            if (obj is null)
            {
                throw new NullReferenceException($"Entity of type {typeof(T).Name} with id {id} was not found");
            }

            return obj;
        }

        public static T ThrowIfNotFound<T>(this T? obj, object id) where T : IEntity
        {
            if (obj is null)
            {
                throw new NullReferenceException($"Entity of type {typeof(T).Name} with {id} was not found");
            }

            return obj;
        }
    }
}
