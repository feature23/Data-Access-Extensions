﻿using System;
using System.Data;
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
    }
}
