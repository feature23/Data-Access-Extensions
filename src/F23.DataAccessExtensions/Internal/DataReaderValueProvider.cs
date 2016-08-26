using System;
using System.Collections.Generic;
using System.Data;

namespace F23.DataAccessExtensions.Internal
{
    internal sealed class DataReaderValueProvider
    {
        private const int ColumnMissing = -1;

        private readonly IDataReader _dataReader;
        private readonly Dictionary<string, int> _columnLookup;

        internal DataReaderValueProvider(IDataReader dataReader)
        {
            if (dataReader == null) throw new ArgumentNullException(nameof(dataReader));

            _dataReader = dataReader;
            _columnLookup = new Dictionary<string, int>();
        }

        internal TValue GetValueOrDefault<TValue>(string key)
        {
            CacheHasColumnIfRequired(key);

            int col;

            if (!_columnLookup.TryGetValue(key, out col) || col == ColumnMissing)
            {
                return default(TValue);
            }

            return !_dataReader.IsDBNull(col) ? (TValue)_dataReader.GetValue(col) : default(TValue);
        }

        internal TValue? GetNullableValueOrDefault<TValue>(string key)
            where TValue : struct
        {
            CacheHasColumnIfRequired(key);

            int col;

            if (_columnLookup.TryGetValue(key, out col) && col != ColumnMissing)
            {
                return _dataReader.IsDBNull(col) ? new TValue?() : (TValue)_dataReader.GetValue(col);
            }

            return new TValue?();
        }

        private void CacheHasColumnIfRequired(string key)
        {
            if (_columnLookup.ContainsKey(key))
            {
                return; // we already checked this column, so bail
            }

            try
            {   
                _columnLookup[key] = _dataReader.GetOrdinal(key);
            }
            catch (IndexOutOfRangeException)
            {
                _columnLookup[key] = ColumnMissing;
            }
        }
    }
}
