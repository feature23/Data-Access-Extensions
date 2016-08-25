using System;
using F23.DataAccessExtensions.UnitTests.Examples;
using System.Collections.Generic;
using System.Data;
using Moq;
using Xunit;

namespace F23.DataAccessExtensions.UnitTests
{   
    public class ParameterTests
    {
        [Fact]
        public void Create_ObjectVersion_GivenNullParameterName_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Parameter.Create(null, 123);
            });
        }

        [Fact]
        public void Create_NullableGenericVersion_GivenNullParameterName_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Parameter.Create(null, (int?)123);
            });
        }

        [Fact]
        public void Create_TableVersion_GivenNullParameterName_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Parameter.Create(null, new List<Customer>());
            });
        }

        [Fact]
        public void Create_ObjectVersion_GivenNullValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", null);

            Assert.NotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.Equal("@foo", iparam.ParameterName);
            Assert.Null(iparam.Value);
        }

        [Fact]
        public void Create_ObjectVersion_GivenIntValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", 123);

            Assert.NotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.Equal("@foo", iparam.ParameterName);
            Assert.Equal(123, iparam.Value);
        }

        [Fact]
        public void Create_ObjectVersion_GivenStringValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", "bar");

            Assert.NotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.Equal("@foo", iparam.ParameterName);
            Assert.Equal("bar", iparam.Value);
        }

        [Fact]
        public void Create_NullableGenericVersion_GivenIntValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", (int?)123);

            Assert.NotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.Equal("@foo", iparam.ParameterName);
            Assert.Equal(123, iparam.Value);
        }

        [Fact]
        public void Create_NullableGenericVersion_GivenNullValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", (int?)null);

            Assert.NotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.Equal("@foo", iparam.ParameterName);
            Assert.Equal(DBNull.Value, iparam.Value);
        }

        [Fact]
        public void Create_TableVersion_GivenNullValue_ShouldCreateParameter()
        {
            var param = Parameter.Create("@foo", (IList<Customer>)null);

            Assert.NotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.Equal("@foo", iparam.ParameterName);
            Assert.Null(iparam.Value);
        }

        [Fact]
        public void Create_TableVersion_GivenList_ShouldCreateParameter()
        {
            var list = new List<Customer>
            {
                new Customer { ID = 123, Name = "Foo" },
                new Customer { ID = 234, Name = "Bar" },
                new Customer { ID = 345, Name = "Baz" }
            };

            var param = Parameter.Create("@foo", list);

            Assert.NotNull(param);

            var cmd = CreateCommandMock();

            var iparam = param.GetParameter(cmd.Object);

            Assert.Equal("@foo", iparam.ParameterName);
            Assert.IsType<DataTable>(iparam.Value);

            var dt = (DataTable)iparam.Value;

            Assert.Equal(3, dt.Rows.Count);
            Assert.Equal(2, dt.Columns.Count);
            Assert.Equal("ID", dt.Columns[0].ColumnName);
            Assert.Equal("Name", dt.Columns[1].ColumnName);
            Assert.Equal(123, dt.Rows[0][0]);
            Assert.Equal("Foo", dt.Rows[0][1]);
            Assert.Equal(234, dt.Rows[1][0]);
            Assert.Equal("Bar", dt.Rows[1][1]); 
            Assert.Equal(345, dt.Rows[2][0]);
            Assert.Equal("Baz", dt.Rows[2][1]);
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
