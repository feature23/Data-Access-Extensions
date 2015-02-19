using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace F23.DataAccessExtensions
{
    public class DbDataParameter
    {
        private readonly string _parameterName;
        private readonly Func<object> _valueFactory;

        private DbDataParameter(string parameterName, Func<object> valueFactory)
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

        public DbDataParameter Create(string parameterName, object value)
        {
            return new DbDataParameter(parameterName, () => value);
        }

        public DbDataParameter Create<T>(string parameterName, T? value)
            where T : struct
        {
            var valueFactory = value.HasValue ? (Func<object>) (() => value.Value) : (() => DBNull.Value);

            return new DbDataParameter(parameterName, valueFactory);
        }

        public DbDataParameter Create<T>(string parameterName, IList<T> tableValues)
            where T : class
        {
            var valueFactory = CreateTableValueFactory(tableValues);

            return new DbDataParameter(parameterName, valueFactory);
        }

        private static Func<object> CreateTableValueFactory<T>(IEnumerable<T> tableValues)
        {
            return () =>
            {
                var dataTable = new DataTable();
                var tableValueProperties = typeof (T)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in tableValueProperties)
                {
                    dataTable.Columns.Add(prop.Name, prop.PropertyType);
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
