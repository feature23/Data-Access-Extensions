using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using F23.DataAccessExtensions.Internal;

namespace F23.DataAccessExtensions
{
    /// <summary>
    /// A collection of extension methods on SqlConnection.
    /// </summary>
    public static class SqlConnectionExtensions
    {
        /// <summary>
        /// Uses SqlBulkCopy to provide a high-performance bulk insert into a SQL Server database table.
        /// </summary>
        /// <param name="connection">The SqlConnection instance.</param>
        /// <param name="tableName">The name of the destination SQL Server database table.</param>
        /// <param name="source">The items to insert.</param>
        /// <typeparam name="TEntity">The type of the items to insert.</typeparam>
        public static void BulkInsert<TEntity>(this SqlConnection connection, string tableName, IEnumerable<TEntity> source)
        {
            using (var bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.WriteToServer(source.ToDataTable());
            }
        }

        /// <summary>
        /// Asynchronously uses SqlBulkCopy to provide a high-performance bulk insert into a SQL Server database table.
        /// </summary>
        /// <param name="connection">The SqlConnection instance.</param>
        /// <param name="tableName">The name of the destination SQL Server database table.</param>
        /// <param name="source">The items to insert.</param>
        /// <typeparam name="TEntity">The type of the items to insert.</typeparam>
        /// <returns>Asynchronously returns</returns>
        public static async Task BulkInsertAsync<TEntity>(this SqlConnection connection, string tableName,
            IEnumerable<TEntity> source)
        {
            using (var bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = tableName;
                await bulkCopy.WriteToServerAsync(source.ToDataTable());
            }
        }
    }
}
