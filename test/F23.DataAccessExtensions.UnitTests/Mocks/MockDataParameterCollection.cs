using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions.UnitTests.Mocks
{
    public class MockDataParameterCollection : ArrayList, IDataParameterCollection
    {
        public bool Contains(string parameterName)
        {
            foreach (IDataParameter item in this)
            {
                if (item.ParameterName == parameterName)
                    return true;
            }

            return false;
        }

        public int IndexOf(string parameterName)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (((IDataParameter)this[i]).ParameterName == parameterName)
                    return i;
            }

            return -1;
        }

        public void RemoveAt(string parameterName)
        {
            var index = IndexOf(parameterName);

            if (index < 0)
                throw new ArgumentOutOfRangeException();

            this.RemoveAt(index);
        }

        public object this[string parameterName]
        {
            get
            {
                return this[IndexOf(parameterName)];
            }
            set
            {
                this[IndexOf(parameterName)] = value;
            }
        }
    }
}
