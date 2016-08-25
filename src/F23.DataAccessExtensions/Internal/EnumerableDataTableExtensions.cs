using System;
using System.Collections.Generic;
using System.Data;

namespace F23.DataAccessExtensions.Internal
{
    internal static class EnumerableDataTableExtensions
    {
        private static readonly Dictionary<Type, object> CachedShredders = new Dictionary<Type, object>();

        internal static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            var shredder = GetOrCreateShredder<T>();

            return shredder.Shred(source, table: null, options: null);
        }

        private static ObjectShredder<T> GetOrCreateShredder<T>()
        {
            if (!CachedShredders.ContainsKey(typeof(T)))
            {
                CachedShredders[typeof(T)] = new ObjectShredder<T>();
            }
            return (ObjectShredder<T>)CachedShredders[typeof(T)];
        }
    }
}
