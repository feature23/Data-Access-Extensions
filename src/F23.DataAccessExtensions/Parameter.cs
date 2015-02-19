using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace F23.DataAccessExtensions
{
    /// <summary>
    /// A helper class that makes it easier to create stored procedure parameters.
    /// </summary>
    public class Parameter
    {
        private readonly string _parameterName;
        private readonly Func<object> _valueFactory;

        private Parameter(string parameterName, Func<object> valueFactory)
        {
            _parameterName = parameterName;
            _valueFactory = valueFactory;
        }

        internal IDbDataParameter GetParameter(IDbCommand command)
        {
            var param = command.CreateParameter();

            param.ParameterName = _parameterName;

            param.Value = _valueFactory();

            return param;
        }

        /// <summary>
        /// Creates a new parameter with the given name and value.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a new parameter.</returns>
        public static Parameter Create(string parameterName, object value)
        {
            return new Parameter(parameterName, () => value);
        }

        /// <summary>
        /// Creates a new parameter with the given name and value.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a new parameter.</returns>
        public static Parameter Create<T>(string parameterName, T? value)
            where T : struct
        {
            var valueFactory = value.HasValue ? (Func<object>) (() => value.Value) : (() => DBNull.Value);

            return new Parameter(parameterName, valueFactory);
        }

        /// <summary>
        /// Creates a new parameter with the given name and value.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="tableValues">The values for a table-valued parameter.</param>
        /// <returns>Returns a new parameter.</returns>
        public static Parameter Create<T>(string parameterName, IList<T> tableValues)
            where T : class
        {
            var valueFactory = CreateTableValueFactory(tableValues);

            return new Parameter(parameterName, valueFactory);
        }

        private static Func<object> CreateTableValueFactory<T>(IEnumerable<T> tableValues)
        {
            return () =>
            {
                var dataTable = new DataTable();
                var tableValueProperties = typeof (T)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(i => i.CanRead)
                    .ToArray();

                foreach (var prop in tableValueProperties)
                {
                    dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                foreach (var item in tableValues)
                {
                    var rowValues = new object[tableValueProperties.Length];
                    for (var i = 0; i < tableValueProperties.Length; i++)
                    {
                        rowValues[i] = tableValueProperties[i].GetValue(item);
                    }

                    dataTable.Rows.Add(rowValues);
                }

                return dataTable;
            };
        }
    }
}
