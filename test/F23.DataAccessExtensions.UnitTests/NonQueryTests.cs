using System;
using NUnit.Framework;
using System.Data;
using Moq;

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

        private static Mock<IDbConnection> CreateMockConnection()
        {
            var conn = new Mock<IDbConnection>();
            conn.SetupAllProperties();

            return conn;
        }
    }
}
