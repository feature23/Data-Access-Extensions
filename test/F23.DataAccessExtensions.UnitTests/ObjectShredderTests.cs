using System;
using System.Linq;
using F23.DataAccessExtensions.Internal;
using Xunit;

namespace F23.DataAccessExtensions.UnitTests
{
    public class ObjectShredderTests
    {
        [Fact]
        public void TestShred()
        {
            var items = Enumerable.Range(1, 10).Select(i => new ShreddableObject
            {
                Id = i,
                Name = "MyName",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = RandomHelper.NextBool() ? DateTime.UtcNow : new DateTime?()
            });

            var dataTable = new ObjectShredder<ShreddableObject>().Shred(items, table: null, options: null);

            Assert.True(dataTable.Columns.Contains("Id"));
            Assert.True(dataTable.Columns.Contains("Name"));
            Assert.True(dataTable.Columns.Contains("CreatedAt"));
            Assert.True(dataTable.Columns.Contains("ModifiedAt"));

            Assert.True(dataTable.Rows.Count == 10);
        }

        private class ShreddableObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? ModifiedAt { get; set; }
        }

        private static class RandomHelper
        {
            private static readonly Random Random = new Random();

            public static bool NextBool() => Random.Next() % 2 == 0;
        }
    }
}
