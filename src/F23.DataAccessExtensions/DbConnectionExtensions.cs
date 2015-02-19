using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using F23.DataAccessExtensions.Commands;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions
{
    public static class DbConnectionExtensions
    {
        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, null, sprocName, parameters);

            return command.Execute();
        }

        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, string sprocName,
            params DbDataParameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, null, sprocName, parameters);

            return command.Execute();
        }

        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params DbDataParameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, null, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, string sprocName,
            params DbDataParameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, null, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
                    params IDbDataParameter[] parameters)
                    where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params DbDataParameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static IList<TEntity> ExecuteSprocSingleColumn<TEntity>(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TEntity>(connection, null, sprocName, parameters);

            return command.Execute();
        }

        public static IList<TEntity> ExecuteSprocSingleColumn<TEntity>(this IDbConnection connection, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TEntity>(connection, null, sprocName, parameters);

            return command.Execute();
        }

        public static IList<TEntity> ExecuteSprocSingleColumn<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
                    params IDbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        public static IList<TEntity> ExecuteSprocSingleColumn<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        public static Task<IList<TEntity>> ExecuteSprocSingleColumnAsync<TEntity>(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TEntity>(connection, null, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<IList<TEntity>> ExecuteSprocSingleColumnAsync<TEntity>(this IDbConnection connection, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TEntity>(connection, null, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<IList<TEntity>> ExecuteSprocSingleColumnAsync<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<IList<TEntity>> ExecuteSprocSingleColumnAsync<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static int ExecuteSprocNonQuery(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, null, sprocName, parameters);

            return command.Execute();
        }

        public static int ExecuteSprocNonQuery(this IDbConnection connection, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, null, sprocName, parameters);

            return command.Execute();
        }

        public static int ExecuteSprocNonQuery(this IDbConnection connection, IDbTransaction transaction, string sprocName,
                    params IDbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        public static int ExecuteSprocNonQuery(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, null, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, null, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, null, sprocName, parameters);

            return command.Execute();
        }

        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, null, sprocName, parameters);

            return command.Execute();
        }

        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, null, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, null, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params DbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }
    }
}
