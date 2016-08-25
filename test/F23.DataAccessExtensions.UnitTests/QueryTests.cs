using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using F23.DataAccessExtensions.UnitTests.Mocks;
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

                conn.ExecuteSproc<object>("Foo");
            });
        }

        [Fact]
        public void ExecuteSproc_GivenNullSprocName_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IDbConnection conn = CreateMockConnection().Object;

                conn.ExecuteSproc<object>(null);
            });
        }

        [Fact]
        public void ExecuteSproc_GivenSprocNameAndNoParams_ShouldReturnResultList()
        {
            // arrange
            IDbCommand cmd;
            IDbConnection conn;
            SetupNonQueryConnectionAndCommand(out cmd, out conn);

            // act
            IList<object> resultList = conn.ExecuteSproc<object>("Foo");

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
            MockDbConnection conn;
            MockDbCommand cmd;
            SetupAsyncNonQueryConnectionAndCommand(out conn, out cmd);

            // act
            IList<object> resultList = await conn.ExecuteSprocAsync<object>("Foo");

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
            IDbCommand cmd;
            IDbConnection conn;
            SetupNonQueryConnectionAndCommand(out cmd, out conn);

            // act
            IList<object> resultList = conn.ExecuteSproc<object>("Foo", Parameter.Create("@bar", 123));

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
            MockDbConnection conn;
            MockDbCommand cmd;
            SetupAsyncNonQueryConnectionAndCommand(out conn, out cmd);

            // act
            IList<object> resultList = await conn.ExecuteSprocAsync<object>("Foo", Parameter.Create("@bar", 123));

            // assert
            Assert.Equal("Foo", cmd.CommandText);
            Assert.Equal(CommandType.StoredProcedure, cmd.CommandType);
            Assert.NotNull(resultList);
            //Assert.Equal(3, affected);
            Assert.Equal("@bar", cmd.Parameters[0].ParameterName);
            Assert.Equal(123, cmd.Parameters[0].Value);
        }
    }
}
