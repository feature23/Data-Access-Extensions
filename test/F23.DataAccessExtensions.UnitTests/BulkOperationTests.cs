//using System;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;
//using Moq;
//using Xunit;

//namespace F23.DataAccessExtensions.UnitTests
//{
//    public class BulkOperationTests
//    {
//        [Fact]
//        public void TestBulkInsert()
//        {
//            var conn = Mock.Of<SqlConnection>();

//            var items = Enumerable.Range(1, 10).Select(i => new ShreddableObject
//            {
//                Id = i,
//                Name = "MyName",
//                CreatedAt = DateTime.UtcNow,
//                ModifiedAt = RandomHelper.NextBool() ? DateTime.UtcNow : new DateTime?()
//            });

//            conn.BulkInsert("ShreddableThings", items);
//        }

//        [Fact]
//        public async Task TestAsyncBulkInsert()
//        {
//            var conn = Mock.Of<SqlConnection>();

//            var items = Enumerable.Range(1, 10).Select(i => new ShreddableObject
//            {
//                Id = i,
//                Name = "MyName",
//                CreatedAt = DateTime.UtcNow,
//                ModifiedAt = RandomHelper.NextBool() ? DateTime.UtcNow : new DateTime?()
//            });

//            await conn.BulkInsertAsync("ShreddableThings", items);
//        }
//    }
//}
