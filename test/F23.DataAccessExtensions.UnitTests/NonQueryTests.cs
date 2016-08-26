using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using F23.DataAccessExtensions.UnitTests.Mocks;
using Xunit;

namespace F23.DataAccessExtensions.UnitTests
{
    public class NonQueryTests : DbConnectionTestBase
    {
        [Fact]
        public void ExecuteSprocNonQuery_GivenNullConnection_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IDbConnection conn = null;

                conn.ExecuteSprocNonQuery("Foo");
            });
        }

        [Fact]
        public void ExecuteSprocNonQuery_GivenNullSprocName_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IDbConnection conn = CreateMockConnection().Object;

                conn.ExecuteSprocNonQuery(null);
            });
        }

        [Fact]
        public void ExecuteSprocNonQuery_GivenSprocNameAndNoParams_ShouldReturnRowsAffected()
        {
            // arrange
            IDbCommand cmd;
            IDbConnection conn;
            SetupNonQueryConnectionAndCommand(out cmd, out conn);

            // act
            int affected = conn.ExecuteSprocNonQuery("Foo");

            // assert
            Assert.Equal("Foo", cmd.CommandText);
            Assert.Equal(CommandType.StoredProcedure, cmd.CommandType);
            Assert.Equal(3, affected);
        }

        [Fact]
        public async Task ExecuteSprocNonQueryAsync_GivenSprocNameAndNoParams_ShouldReturnRowsAffected()
        {
            // arrange
            MockDbConnection conn;
            MockDbCommand cmd;
            SetupAsyncNonQueryConnectionAndCommand(out conn, out cmd);

            // act
            int affected = await conn.ExecuteSprocNonQueryAsync("Foo");

            // assert
            Assert.Equal("Foo", cmd.CommandText);
            Assert.Equal(CommandType.StoredProcedure, cmd.CommandType);
            Assert.Equal(3, affected);
        }

        [Fact]
        public void ExecuteSprocNonQuery_GivenSprocNameAndParams_ShouldReturnRowsAffected()
        {
            // arrange
            IDbCommand cmd;
            IDbConnection conn;
            SetupNonQueryConnectionAndCommand(out cmd, out conn);

            // act
            int affected = conn.ExecuteSprocNonQuery("Foo", Parameter.Create("@bar", 123));

            // assert
            Assert.Equal("Foo", cmd.CommandText);
            Assert.Equal(CommandType.StoredProcedure, cmd.CommandType);
            Assert.Equal(3, affected);
            Assert.Equal("@bar", ((IDataParameter)cmd.Parameters[0]).ParameterName);
            Assert.Equal(123, ((IDataParameter)cmd.Parameters[0]).Value);
        }

        [Fact]
        public async Task ExecuteSprocNonQueryAsync_GivenSprocNameAndParam_ShouldReturnRowsAffected()
        {
            // arrange
            MockDbConnection conn;
            MockDbCommand cmd;
            SetupAsyncNonQueryConnectionAndCommand(out conn, out cmd);

            // act
            int affected = await conn.ExecuteSprocNonQueryAsync("Foo", Parameter.Create("@bar", 123));

            // assert
            Assert.Equal("Foo", cmd.CommandText);
            Assert.Equal(CommandType.StoredProcedure, cmd.CommandType);
            Assert.Equal(3, affected);
            Assert.Equal("@bar", cmd.Parameters[0].ParameterName);
            Assert.Equal(123, cmd.Parameters[0].Value);
        }

        private static void SetupAsyncNonQueryConnectionAndCommand(out MockDbConnection conn, out MockDbCommand cmd)
        {
            conn = new MockDbConnection();

            cmd = new MockDbCommand
            {
                MockExecuteNonQueryAsync = c => Task.FromResult(3)
            };

            DbCommand ret = cmd;

            conn.MockCreateDbCommand = () => ret;

            cmd.Connection = conn;
        }

        private void SetupNonQueryConnectionAndCommand(out IDbCommand cmd, out IDbConnection conn)
        {
            var mockConn = CreateMockConnection();

            var mockCmd = CreateMockIDbCommand();

            cmd = mockCmd.Object;

            mockConn.Setup(i => i.CreateCommand()).Returns(cmd);

            conn = mockConn.Object;
            cmd.Connection = conn;
        }
    }
}
