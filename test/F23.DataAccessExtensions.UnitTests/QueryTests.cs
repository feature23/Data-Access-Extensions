using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using F23.DataAccessExtensions.UnitTests.Mocks;
using Moq;
using Xunit;

namespace F23.DataAccessExtensions.UnitTests
{
    public class QueryTests : DbConnectionTestBase
    {
        [Fact]
        public void ExecuteSproc_GivenNullConnection_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => 
            {
                IDbConnection conn = null;

                conn.ExecuteSproc<TestResultType>("Foo");
            });
        }

        [Fact]
        public void ExecuteSproc_GivenNullSprocName_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IDbConnection conn = CreateMockConnection().Object;

                conn.ExecuteSproc<TestResultType>(null);
            });
        }

        [Fact]
        public void ExecuteSproc_GivenSprocNameAndNoParams_ShouldReturnResultList()
        {
            // arrange
            DbCommand cmd;
            DbConnection conn;
            SetupQueryConnectionAndCommand(out cmd, out conn);

            // act
            IList<TestResultType> resultList = conn.ExecuteSproc<TestResultType>("Foo");

            // assert
            Assert.Equal("Foo", cmd.CommandText);
            Assert.Equal(CommandType.StoredProcedure, cmd.CommandType);
            Assert.NotNull(resultList);
            //Assert.Equal(3, affected);
        }

        [Fact]
        public async Task ExecuteSprocAsync_GivenSprocNameAndNoParams_ShouldReturnResultList()
        {
            // arrange
            DbConnection conn;
            DbCommand cmd;
            SetupAsyncQueryConnectionAndCommand(out conn, out cmd);

            // act
            IList<TestResultType> resultList = await conn.ExecuteSprocAsync<TestResultType>("Foo");

            // assert
            Assert.Equal("Foo", cmd.CommandText);
            Assert.Equal(CommandType.StoredProcedure, cmd.CommandType);
            Assert.NotNull(resultList);
            //Assert.Equal(3, affected);
        }

        [Fact]
        public void ExecuteSproc_GivenSprocNameAndParams_ShouldReturnResultList()
        {
            // arrange
            DbCommand cmd;
            DbConnection conn;
            SetupQueryConnectionAndCommand(out cmd, out conn);

            // act
            IList<TestResultType> resultList = conn.ExecuteSproc<TestResultType>("Foo", Parameter.Create("@bar", 123));

            // assert
            Assert.Equal("Foo", cmd.CommandText);
            Assert.Equal(CommandType.StoredProcedure, cmd.CommandType);
            Assert.NotNull(resultList);
            //Assert.Equal(3, affected);
            Assert.Equal("@bar", ((IDataParameter)cmd.Parameters[0]).ParameterName);
            Assert.Equal(123, ((IDataParameter)cmd.Parameters[0]).Value);
        }

        [Fact]
        public async Task ExecuteSprocAsync_GivenSprocNameAndParam_ShouldReturnResultList()
        {
            // arrange
            DbConnection conn;
            DbCommand cmd;
            SetupAsyncQueryConnectionAndCommand(out conn, out cmd);

            // act
            IList<TestResultType> resultList = await conn.ExecuteSprocAsync<TestResultType>("Foo", Parameter.Create("@bar", 123));

            // assert
            Assert.Equal("Foo", cmd.CommandText);
            Assert.Equal(CommandType.StoredProcedure, cmd.CommandType);
            Assert.NotNull(resultList);
            //Assert.Equal(3, affected);
            Assert.Equal("@bar", cmd.Parameters[0].ParameterName);
            Assert.Equal(123, cmd.Parameters[0].Value);
        }

        private class TestResultType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? ModifiedAt { get; set; }
        }

        private static void SetupAsyncQueryConnectionAndCommand(out DbConnection conn, out DbCommand cmd)
        {
            var mockConnection = new MockDbConnection();

            var mockCmd = new MockDbCommand
            {
                MockExecuteDbDataReaderAsync = (commandBehavior, cancelToken) => Task.FromResult(Mock.Of<DbDataReader>())
            };

            DbCommand ret = mockCmd;

            mockConnection.MockCreateDbCommand = () => ret;

            mockCmd.Connection = mockConnection;

            conn = mockConnection;
            cmd = mockCmd;
        }

        private void SetupQueryConnectionAndCommand(out DbCommand cmd, out DbConnection conn)
        {
            var mockConnection = new MockDbConnection();

            var mockCmd = new MockDbCommand
            {
                MockExecuteDbDataReader = commandBehavior => Mock.Of<DbDataReader>()
            };

            DbCommand ret = mockCmd;

            mockConnection.MockCreateDbCommand = () => ret;

            mockCmd.Connection = mockConnection;

            conn = mockConnection;
            cmd = mockCmd;
        }
    }
}
