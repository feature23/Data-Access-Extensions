using System;
using NUnit.Framework;
using System.Data;
using Moq;
using System.Threading.Tasks;
using F23.DataAccessExtensions.UnitTests.Mocks;
using System.Data.Common;
using System.Threading;

namespace F23.DataAccessExtensions.UnitTests
{
    [TestFixture]
    public class NonQueryTests
    {
        [Test]
        public void ExecuteSprocNonQuery_GivenNullConnection_ShouldThrow()
        {
            try
            {
                IDbConnection conn = null;

                conn.ExecuteSprocNonQuery("Foo");
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }

            Assert.Fail("An argument null exception was not thrown.");
        }

        [Test]
        public void ExecuteSprocNonQuery_GivenNullSprocName_ShouldThrow()
        {
            try
            {
                IDbConnection conn = CreateMockConnection().Object;

                conn.ExecuteSprocNonQuery(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }

            Assert.Fail("An argument null exception was not thrown.");
        }

        [Test]
        public void ExecuteSprocNonQuery_GivenSprocNameAndNoParams_ShouldReturnRowsAffected()
        {
            // arrange
            var mockConn = CreateMockConnection();

            var mockCmd = CreateMockIDbCommand();
            mockCmd.Setup(i => i.ExecuteNonQuery()).Returns(3);

            var cmd = mockCmd.Object;
            
            mockConn.Setup(i => i.CreateCommand()).Returns(cmd);

            var conn = mockConn.Object;
            cmd.Connection = conn;

            // act
            int affected = conn.ExecuteSprocNonQuery("Foo");

            // assert
            Assert.AreEqual("Foo", cmd.CommandText);
            Assert.AreEqual(CommandType.StoredProcedure, cmd.CommandType);
            Assert.AreEqual(3, affected);
        }

        [Test]
        public async Task ExecuteSprocNonQueryAsync_GivenSprocNameAndNoParams_ShouldReturnRowsAffected()
        {
            // arrange
            var conn = new MockDbConnection();

            var mockCmd = CreateMockDbCommand();
            mockCmd.Setup(i => i.ExecuteNonQueryAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(3));

            var cmd = mockCmd.Object;

            conn.MockCreateDbCommand = () => cmd;

            cmd.Connection = conn;

            // act
            int affected = await conn.ExecuteSprocNonQueryAsync("Foo");

            // assert
            Assert.AreEqual("Foo", cmd.CommandText);
            Assert.AreEqual(CommandType.StoredProcedure, cmd.CommandType);
            Assert.AreEqual(3, affected);
        }

        [Test]
        public void ExecuteSprocNonQuery_GivenSprocNameAndParams_ShouldReturnRowsAffected()
        {
            // arrange
            var mockConn = CreateMockConnection();

            var mockCmd = CreateMockIDbCommand();
            mockCmd.Setup(i => i.ExecuteNonQuery()).Returns(3);
            mockCmd.Setup(i => i.CreateParameter()).Returns(() =>
            {
                return new MockDbParameter();
            });

            var cmd = mockCmd.Object;

            mockConn.Setup(i => i.CreateCommand()).Returns(cmd);

            var conn = mockConn.Object;
            cmd.Connection = conn;

            // act
            int affected = conn.ExecuteSprocNonQuery("Foo", Parameter.Create("@bar", 123));

            // assert
            Assert.AreEqual("Foo", cmd.CommandText);
            Assert.AreEqual(CommandType.StoredProcedure, cmd.CommandType);
            Assert.AreEqual(3, affected);
            Assert.AreEqual("@bar", ((IDataParameter)cmd.Parameters[0]).ParameterName);
            Assert.AreEqual(123, ((IDataParameter)cmd.Parameters[0]).Value);
        }

        [Test]
        public async Task ExecuteSprocNonQueryAsync_GivenSprocNameAndParam_ShouldReturnRowsAffected()
        {
            // arrange
            var conn = new MockDbConnection();

            var cmd = new MockDbCommand();
            cmd.MockExecuteNonQueryAsync = c => Task.FromResult(3);
            
            conn.MockCreateDbCommand = () => cmd;

            cmd.Connection = conn;

            // act
            int affected = await conn.ExecuteSprocNonQueryAsync("Foo", Parameter.Create("@bar", 123));

            // assert
            Assert.AreEqual("Foo", cmd.CommandText);
            Assert.AreEqual(CommandType.StoredProcedure, cmd.CommandType);
            Assert.AreEqual(3, affected);
            Assert.AreEqual("@bar", cmd.Parameters[0].ParameterName);
            Assert.AreEqual(123, cmd.Parameters[0].Value);
        }

        private Mock<DbParameter> CreateMockDbParameter()
        {
            var p = new Mock<DbParameter>();
            p.SetupAllProperties();

            return p;
        }

        private Mock<IDbDataParameter> CreateMockIDbDataParameter()
        {
            var p = new Mock<IDbDataParameter>();
            p.SetupAllProperties();

            return p;
        }

        private Mock<IDbCommand> CreateMockIDbCommand()
        {
            var cmd = new Mock<IDbCommand>();
            var collection = new MockDataParameterCollection();
            cmd.SetupAllProperties();
            cmd.SetupGet(i => i.Parameters).Returns(collection);
            
            return cmd;
        }

        private Mock<DbCommand> CreateMockDbCommand()
        {
            var cmd = new Mock<DbCommand>();
            cmd.SetupAllProperties();

            return cmd;
        }

        private static Mock<IDbConnection> CreateMockConnection()
        {
            var conn = new Mock<IDbConnection>();
            conn.SetupAllProperties();

            return conn;
        }
    }
}
