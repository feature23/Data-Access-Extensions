using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions.UnitTests.Mocks
{
    public class MockDbCommand : DbCommand
    {
        private MockDataParameterCollection _parameters = new MockDataParameterCollection();

        public override void Cancel()
        {
            
        }

        public override string CommandText
        {
            get;
            set;
        }

        public override int CommandTimeout
        {
            get;
            set;
        }

        public override System.Data.CommandType CommandType
        {
            get;
            set;
        }

        protected override DbParameter CreateDbParameter()
        {
            return new MockDbParameter();
        }

        protected override DbConnection DbConnection
        {
            get;
            set;
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { return _parameters; }
        }

        protected override DbTransaction DbTransaction
        {
            get;
            set;
        }

        public override bool DesignTimeVisible
        {
            get;
            set;
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return MockExecuteDbDataReader(behavior);
        }

        public Func<CommandBehavior, DbDataReader> MockExecuteDbDataReader { get; set; }

        public override int ExecuteNonQuery()
        {
            return MockExecuteNonQuery();
        }

        public Func<int> MockExecuteNonQuery { get; set; }

        public override object ExecuteScalar()
        {
            return MockExecuteScalar();
        }

        public Func<object> MockExecuteScalar { get; set; }

        public override void Prepare()
        {
            
        }

        public override System.Data.UpdateRowSource UpdatedRowSource
        {
            get;
            set;
        }

        protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            return MockExecuteDbDataReaderAsync(behavior, cancellationToken);
        }

        public Func<CommandBehavior, CancellationToken, Task<DbDataReader>> MockExecuteDbDataReaderAsync { get; set; }

        public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            return MockExecuteNonQueryAsync(cancellationToken);
        }

        public Func<CancellationToken, Task<int>> MockExecuteNonQueryAsync { get; set; }

        public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            return MockExecuteScalarAsync(cancellationToken);
        }

        public Func<CancellationToken, Task<object>> MockExecuteScalarAsync { get; set; }
    }
}
