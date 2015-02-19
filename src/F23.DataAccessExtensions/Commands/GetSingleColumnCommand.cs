using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions.Commands
{
    internal sealed class GetSingleColumnCommand<TEntity> : StoredProcedureCommandBase<IList<TEntity>>
    {
        public GetSingleColumnCommand(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<IDbDataParameter> parameters)
            : base(connection, transaction, storedProcedureName, parameters)
        {
        }

        public GetSingleColumnCommand(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<DbDataParameter> parameters)
            : base(connection, transaction, storedProcedureName, parameters)
        {
        }

        protected internal override IList<TEntity> ExecuteInternal(IDbCommand dbCommand)
        {
            var result = new List<TEntity>();

            using (var reader = dbCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = (TEntity)reader.GetValue(0);
                    result.Add(item);
                }

                reader.Close();
            }

            return result;
        }

        protected internal async override Task<IList<TEntity>> ExecuteInternalAsync(DbCommand dbCommand)
        {
            var result = new List<TEntity>();

            using (var reader = await dbCommand.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var item = (TEntity)reader.GetValue(0);
                    result.Add(item);
                }

                reader.Close();
            }

            return result;
        }
    }
}
