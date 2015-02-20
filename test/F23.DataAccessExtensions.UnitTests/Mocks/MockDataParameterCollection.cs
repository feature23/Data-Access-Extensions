using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions.UnitTests.Mocks
{
    public class MockDataParameterCollection : DbParameterCollection
    {
        private readonly ArrayList _values = new ArrayList();

        public override int Add(object value)
        {
            return _values.Add(value);
        }

        public override void AddRange(Array values)
        {
            _values.AddRange(values);
        }

        public override void Clear()
        {
            _values.Clear();
        }

        public override bool Contains(string value)
        {
            return _values.Cast<IDataParameter>().Any(i => i.ParameterName == value);
        }

        public override bool Contains(object value)
        {
            return _values.Contains(value);
        }

        public override void CopyTo(Array array, int index)
        {
            _values.CopyTo(array, index);
        }

        public override int Count
        {
            get { return _values.Count; }
        }

        public override IEnumerator GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            return _values.Cast<DbParameter>().FirstOrDefault(i => i.ParameterName == parameterName);
        }

        protected override DbParameter GetParameter(int index)
        {
            return (DbParameter)_values[index];
        }

        public override int IndexOf(string parameterName)
        {
            // don't really care about algorithmic efficiency here in these unit tests
            return _values.IndexOf(_values.Cast<DbParameter>().FirstOrDefault(i => i.ParameterName == parameterName));
        }

        public override int IndexOf(object value)
        {
            return _values.IndexOf(value);
        }

        public override void Insert(int index, object value)
        {
            _values.Insert(index, value);
        }

        public override bool IsFixedSize
        {
            get { return _values.IsFixedSize; }
        }

        public override bool IsReadOnly
        {
            get { return _values.IsReadOnly; }
        }

        public override bool IsSynchronized
        {
            get { return _values.IsSynchronized; }
        }

        public override void Remove(object value)
        {
            _values.Remove(value);
        }

        public override void RemoveAt(string parameterName)
        {
            _values.Remove(_values.Cast<DbParameter>().FirstOrDefault(i => i.ParameterName == parameterName));
        }

        public override void RemoveAt(int index)
        {
            _values.RemoveAt(index);
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            var index = IndexOf(parameterName);
            _values[index] = value;
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            _values[index] = value;
        }

        public override object SyncRoot
        {
            get { return _values.SyncRoot; }
        }
    }
}
