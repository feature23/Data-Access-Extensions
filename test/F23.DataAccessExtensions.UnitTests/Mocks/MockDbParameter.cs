using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions.UnitTests.Mocks
{
    public class MockDbParameter : DbParameter
    {
        public override System.Data.DbType DbType
        {
            get;
            set;
        }

        public override System.Data.ParameterDirection Direction
        {
            get;
            set;
        }

        public override bool IsNullable
        {
            get;
            set;
        }

        public override string ParameterName
        {
            get;
            set;
        }

        public override void ResetDbType()
        {
            
        }

        public override int Size
        {
            get;
            set;
        }

        public override string SourceColumn
        {
            get;
            set;
        }

        public override bool SourceColumnNullMapping
        {
            get;
            set;
        }

        public override System.Data.DataRowVersion SourceVersion
        {
            get;
            set;
        }

        public override object Value
        {
            get;
            set;
        }
    }
}
