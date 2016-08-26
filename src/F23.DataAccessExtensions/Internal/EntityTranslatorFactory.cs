using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace F23.DataAccessExtensions.Internal
{
    internal sealed class EntityTranslatorFactory
    {
        private static readonly Dictionary<Type, EntityTranslator> EntityFactoryCache;

        static EntityTranslatorFactory()
        {
            EntityFactoryCache = new Dictionary<Type, EntityTranslator>();
        }

        public static EntityTranslator<TEntity> CreateEntityFactory<TEntity>()
        {
            var type = typeof (TEntity);

            CacheEntityFactoryIfRequired(type);

            return EntityFactoryCache[type] as EntityTranslator<TEntity>;
        }

        private static void CacheEntityFactoryIfRequired(Type type)
        {
            if (EntityFactoryCache.ContainsKey(type))
            {
                return; // already cached, so bail
            }

            EntityFactoryCache[type] = CreateEntityTranslator(type);
        }

        private static EntityTranslator CreateEntityTranslator(Type type)
        {
            var ctor = Expression.New(type);

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.CanWrite);
            
            Type baseType = type.BaseType;

            while (baseType != null && baseType != typeof(object))
            {
                var baseProperties = baseType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(prop => prop.CanWrite);

                properties = properties
                    .Where(p => !baseProperties.Select(bp => bp.Name).Contains(p.Name))
                    .Concat(baseProperties);

                baseType = baseType.BaseType;
            }

            var pValueProvider = Expression.Parameter(typeof(DataReaderValueProvider));

            var propertySetters = (from prop in properties
                                   let readerMethod = MakeReaderMethod(prop.PropertyType)
                                   let newValue = Expression.Call(pValueProvider, readerMethod, Expression.Constant(prop.Name))
                                   let castValue = Expression.Convert(newValue, prop.PropertyType)
                                   select Expression.Bind(prop.SetMethod, castValue)).Cast<MemberBinding>().ToList();

            var init = Expression.MemberInit(ctor, propertySetters);

            var factory = Expression.Lambda<EntityTranslator>(init, pValueProvider);

            return factory.Compile();
        }

        private static MethodInfo MakeReaderMethod(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);

            var isNullable = underlyingType != null;

            var methodName = isNullable
                ? nameof(DataReaderValueProvider.GetNullableValueOrDefault)
                : nameof(DataReaderValueProvider.GetValueOrDefault);

            var genericType = isNullable ? underlyingType : type;

            return typeof (DataReaderValueProvider)
                .GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(genericType);
        }
    }
}
