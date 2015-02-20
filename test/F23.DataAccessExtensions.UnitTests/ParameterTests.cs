using System;
using NUnit.Framework;
using F23.DataAccessExtensions.UnitTests.Examples;
using System.Collections.Generic;
using System.Data;
using Moq;

namespace F23.DataAccessExtensions.UnitTests
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void Create_ObjectVersion_GivenNullParameterName_ShouldThrow()
        {
            try
            {
                var parm = Parameter.Create(null, (object)123);
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }

            Assert.Fail("An ArgumentNullException was not thrown");
        }

        [Test]
        public void Create_NullableGenericVersion_GivenNullParameterName_ShouldThrow()
        {
            try
            {
                var parm = Parameter.Create(null, (int?)123);
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }

            Assert.Fail("An ArgumentNullException was not thrown");
        }

        [Test]
        public void Create_TableVersion_GivenNullParameterName_ShouldThrow()
        {
            try
            {
                var parm = Parameter.Create(null, new List<Customer>());
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }

            Assert.Fail("An ArgumentNullException was not thrown");
        }

        [Test]
        public void Create_ObjectVersion_GivenNullValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", null);

            Assert.IsNotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.AreEqual("@foo", iparam.ParameterName);
            Assert.IsNull(iparam.Value);
        }

        [Test]
        public void Create_ObjectVersion_GivenIntValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", 123);

            Assert.IsNotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.AreEqual("@foo", iparam.ParameterName);
            Assert.AreEqual(123, iparam.Value);
        }

        [Test]
        public void Create_ObjectVersion_GivenStringValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", "bar");

            Assert.IsNotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.AreEqual("@foo", iparam.ParameterName);
            Assert.AreEqual("bar", iparam.Value);
        }

        [Test]
        public void Create_NullableGenericVersion_GivenIntValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", (int?)123);

            Assert.IsNotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.AreEqual("@foo", iparam.ParameterName);
            Assert.AreEqual(123, iparam.Value);
        }

        [Test]
        public void Create_NullableGenericVersion_GivenNullValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", (int?)null);

            Assert.IsNotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.AreEqual("@foo", iparam.ParameterName);
            Assert.AreEqual(DBNull.Value, iparam.Value);
        }

        [Test]
        public void Create_TableVersion_GivenNullValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", (IList<Customer>)null);

            Assert.IsNotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.AreEqual("@foo", iparam.ParameterName);
            Assert.IsNull(iparam.Value);
        }

        [Test]
        public void Create_TableVersion_GivenList_ShouldCreateParameter()
        {
            var list = new List<Customer>
            {
                new Customer { ID = 123, Name = "Foo" },
                new Customer { ID = 234, Name = "Bar" },
                new Customer { ID = 345, Name = "Baz" }
            };

            var param = Parameter.Create("@foo", list);

            Assert.IsNotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.AreEqual("@foo", iparam.ParameterName);
            Assert.IsInstanceOf<DataTable>(iparam.Value);

            DataTable dt = iparam.Value as DataTable;

            Assert.AreEqual(3, dt.Rows.Count);
            Assert.AreEqual(2, dt.Columns.Count);
            Assert.AreEqual("ID", dt.Columns[0].ColumnName);
            Assert.AreEqual("Name", dt.Columns[1].ColumnName);
            Assert.AreEqual(123, dt.Rows[0][0]);
            Assert.AreEqual("Foo", dt.Rows[0][1]);
            Assert.AreEqual(234, dt.Rows[1][0]);
            Assert.AreEqual("Bar", dt.Rows[1][1]); 
            Assert.AreEqual(345, dt.Rows[2][0]);
            Assert.AreEqual("Baz", dt.Rows[2][1]);
        }

        private static Mock<IDbCommand> CreateCommandMock()
        {
            var mockParam = new Mock<IDbDataParameter>();
            mockParam.SetupAllProperties();

            var cmd = new Mock<IDbCommand>();
            cmd.SetupAllProperties();
            cmd.Setup(i => i.CreateParameter()).Returns(mockParam.Object);
            return cmd;
        }
    }
}
