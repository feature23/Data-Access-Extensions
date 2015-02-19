using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace F23.DataAccessExtensions.Commands
{
    internal sealed class GetXDocumentFromScalarCommand : StoredProcedureCommandBase<XDocument>
    {
        public GetXDocumentFromScalarCommand(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<IDbDataParameter> parameters) 
            : base(connection, transaction, storedProcedureName, parameters)
        {
        }

        public GetXDocumentFromScalarCommand(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<Parameter> parameters)
            : base(connection, transaction, storedProcedureName, parameters)
        {
        }

        protected internal override XDocument ExecuteInternal(IDbCommand dbCommand)
        {
            var sqlCommand = dbCommand as SqlCommand;

            if (sqlCommand == null)
            {
                throw new NotSupportedException("XDocument result is only supported for Microsoft SQL Server commands.");
            }

            using (var reader = sqlCommand.ExecuteReader())
            {
                var sb = new StringBuilder();

                while (reader.Read())
                {
                    sb.Append(reader.GetString(0));
                }

                return XDocument.Parse(sb.ToString());
            }
        }

        protected internal async override Task<XDocument> ExecuteInternalAsync(DbCommand dbCommand)
        {
            var sqlCommand = dbCommand as SqlCommand;

            if (sqlCommand == null)
            {
                throw new NotSupportedException("XDocument result is only supported for Microsoft SQL Server commands.");
            }

            using (var reader = await sqlCommand.ExecuteReaderAsync())
            {
                var sb = new StringBuilder();

                while (await reader.ReadAsync())
                {
                    sb.Append(reader.GetString(0));
                }

                return XDocument.Parse(sb.ToString());
            }
        }
    }
}
