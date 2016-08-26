using System;
using System.Data;
using System.Data.Common;

namespace F23.DataAccessExtensions.UnitTests.Mocks
{
    public class MockDbConnection : DbConnection
    {
        private ConnectionState _state = ConnectionState.Closed;

        protected override DbCommand CreateDbCommand()
        {
            return MockCreateDbCommand();
        }

        public Func<DbCommand> MockCreateDbCommand { get; set; }
        
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            _state = ConnectionState.Closed;
        }

        public override string ConnectionString { get; set; }

        public override string DataSource => "DataSource";

        public override string Database => "Database";

        public override void Open()
        {
            _state = ConnectionState.Open;
        }

        public override string ServerVersion => "Version";

        public override ConnectionState State => _state;
    }
}
