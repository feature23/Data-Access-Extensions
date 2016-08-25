using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace F23.DataAccessExtensions.Internal
{
    internal class ObjectShredder<T>
    {
        private const string PrimitiveColumnName = "Value";

        private readonly FieldInfo[] _fi;
        private readonly PropertyInfo[] _pi;
        private readonly Dictionary<string, int> _ordinalMap;

        internal ObjectShredder()
        {
            var type = typeof(T);

            _fi = type.GetFields();
            _pi = type.GetProperties();
            _ordinalMap = new Dictionary<string, int>();
        }

        /// <summary>
        /// Loads a DataTable from a sequence of objects.
        /// </summary>
        /// <param name="source">The sequence of objects to load into the DataTable.</param>
        /// <param name="table">The input table. The schema of the table must match that 
        /// the type T.  If the table is null, a new table is created with a schema 
        /// created from the public properties and fields of the type T.</param>
        /// <param name="options">Specifies how values from the source sequence will be applied to 
        /// existing rows in the table.</param>
        /// <returns>A DataTable created from the source sequence.</returns>
        internal DataTable Shred(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Load the table from the scalar sequence if T is a primitive type.
            if (typeof(T).IsPrimitive)
            {
                return ShredPrimitive(source, table, options);
            }

            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            // Initialize the ordinal map and extend the table schema based on type T.
            table = ExtendTable(table, typeof(T));

            // Enumerate the source sequence and load the object values into rows.
            table.BeginLoadData();
            foreach (var item in source)
            {
                if (options != null)
                {
                    table.LoadDataRow(ShredObject(table, item), (LoadOption)options);
                }
                else
                {
                    table.LoadDataRow(ShredObject(table, item), true);
                }
            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        private static DataTable ShredPrimitive(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            if (!table.Columns.Contains(PrimitiveColumnName))
            {
                table.Columns.Add(PrimitiveColumnName, typeof(T));
            }

            // Enumerate the source sequence and load the scalar values into rows.
            table.BeginLoadData();
            foreach (var item in source)
            {
                var values = new object[table.Columns.Count];

                values[table.Columns[PrimitiveColumnName].Ordinal] = item;

                if (options != null)
                {
                    table.LoadDataRow(values, (LoadOption)options);
                }
                else
                {
                    table.LoadDataRow(values, true);
                }
            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        private object[] ShredObject(DataTable table, T instance)
        {
            var fi = _fi;
            var pi = _pi;

            if (instance.GetType() != typeof(T))
            {
                // If the instance is derived from T, extend the table schema
                // and get the properties and fields.
                ExtendTable(table, instance.GetType());
                fi = instance.GetType().GetFields();
                pi = instance.GetType().GetProperties();
            }

            // Add the property and field values of the instance to an array.
            var values = new object[table.Columns.Count];

            foreach (var f in fi)
            {
                values[_ordinalMap[f.Name]] = f.GetValue(instance) ?? DBNull.Value;
            }

            foreach (var p in pi)
            {
                values[_ordinalMap[p.Name]] = p.GetValue(instance, null) ?? DBNull.Value;
            }

            // Return the property and field values of the instance.
            return values;
        }

        private DataTable ExtendTable(DataTable table, Type type)
        {
            // Extend the table schema if the input table was null or if the value 
            // in the sequence is derived from type T.            
            foreach (var f in type.GetFields())
            {
                if (_ordinalMap.ContainsKey(f.Name)) continue;

                // Add the field as a column in the table if it doesn't exist
                // already.
                var dc = table.Columns.Contains(f.Name)
                    ? table.Columns[f.Name]
                    : table.Columns.Add(f.Name, Nullable.GetUnderlyingType(f.FieldType) ?? f.FieldType);

                // Add the field to the ordinal map.
                _ordinalMap.Add(f.Name, dc.Ordinal);
            }

            foreach (var p in type.GetProperties())
            {
                if (_ordinalMap.ContainsKey(p.Name)) continue;

                // Add the property as a column in the table if it doesn't exist
                // already.
                var dc = table.Columns.Contains(p.Name)
                    ? table.Columns[p.Name]
                    : table.Columns.Add(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);

                // Add the property to the ordinal map.
                _ordinalMap.Add(p.Name, dc.Ordinal);
            }

            // Return the table.
            return table;
        }
    }
}