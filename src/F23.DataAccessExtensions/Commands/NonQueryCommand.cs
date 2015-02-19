using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions.Commands
{
    internal sealed class NonQueryCommand : StoredProcedureCommandBase<int>
    {
        public NonQueryCommand(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<IDbDataParameter> parameters)
            : base(connection, transaction, storedProcedureName, parameters)
        {
        }

        public NonQueryCommand(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<Parameter> parameters)
            : base(connection, transaction, storedProcedureName, parameters)
        {
        }

        protected internal override int ExecuteInternal(IDbCommand dbCommand)
        {
            return dbCommand.ExecuteNonQuery();
        }

        protected internal override Task<int> ExecuteInternalAsync(DbCommand dbCommand)
        {
            return dbCommand.ExecuteNonQueryAsync();
        }
    }
}
