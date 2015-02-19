using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace F23.DataAccessExtensions
{
    internal sealed class EntityFactoryFactory
    {
        private static readonly Dictionary<Type, EntityFactory> EntityFactoryCache;

        static EntityFactoryFactory()
        {
            EntityFactoryCache = new Dictionary<Type, EntityFactory>();
        }

        public static EntityFactory<TEntity> CreateEntityFactory<TEntity>()
        {
            var type = typeof (TEntity);

            CacheEntityFactoryIfRequired(type);

            return EntityFactoryCache[type] as EntityFactory<TEntity>;
        }

        private static void CacheEntityFactoryIfRequired(Type type)
        {
            if (EntityFactoryCache.ContainsKey(type))
            {
                return; // already cached, so bail
            }

            EntityFactoryCache[type] = CreateEntityFactoryDelegate(type);
        }

        private static EntityFactory CreateEntityFactoryDelegate(Type type)
        {
            var ctor = Expression.New(type);

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.CanWrite);

            var pValueProvider = Expression.Parameter(typeof(DataReaderValueProvider));

            var propertySetters = (from prop in properties
                                   let readerMethod = MakeReaderMethod(prop.PropertyType)
                                   let newValue = Expression.Call(pValueProvider, readerMethod, Expression.Constant(prop.Name))
                                   let castValue = Expression.Convert(newValue, prop.PropertyType)
                                   select Expression.Bind(prop.SetMethod, castValue)).Cast<MemberBinding>().ToList();

            var init = Expression.MemberInit(ctor, propertySetters);

            var factory = Expression.Lambda<EntityFactory>(init, pValueProvider);

            return factory.Compile();
        }

        private static MethodInfo MakeReaderMethod(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            var isNullable = underlyingType != null;
            var methodName = isNullable ? "GetNullableValueOrDefault" : "GetValueOrDefault";

            var genericType = isNullable ? underlyingType : type;

            return typeof (DataReaderValueProvider)
                .GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance)
                .MakeGenericMethod(genericType);
        }
    }
}
