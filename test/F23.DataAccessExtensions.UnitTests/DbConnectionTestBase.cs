using System.Data;
using System.Data.Common;
using F23.DataAccessExtensions.UnitTests.Mocks;
using Moq;

namespace F23.DataAccessExtensions.UnitTests
{
    public abstract class DbConnectionTestBase
    {
        protected Mock<DbParameter> CreateMockDbParameter()
        {
            var p = new Mock<DbParameter>();
            p.SetupAllProperties();

            return p;
        }

        protected Mock<IDbDataParameter> CreateMockIDbDataParameter()
        {
            var p = new Mock<IDbDataParameter>();
            p.SetupAllProperties();

            return p;
        }

        protected static Mock<IDbCommand> CreateMockIDbCommand()
        {
            var cmd = new Mock<IDbCommand>();
            var collection = new MockDataParameterCollection();

            cmd.SetupAllProperties();
            cmd.SetupGet(i => i.Parameters).Returns(collection);
            cmd.Setup(i => i.ExecuteNonQuery()).Returns(3);
            cmd.Setup(i => i.CreateParameter()).Returns(() => new MockDbParameter());

            return cmd;
        }

        protected static Mock<DbCommand> CreateMockDbCommand()
        {
            var cmd = new Mock<DbCommand>();
            cmd.SetupAllProperties();

            return cmd;
        }

        protected static Mock<IDbConnection> CreateMockConnection()
        {
            var conn = new Mock<IDbConnection>();
            conn.SetupAllProperties();

            return conn;
        }
    }
}
