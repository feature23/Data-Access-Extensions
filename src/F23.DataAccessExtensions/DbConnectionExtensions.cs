using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using F23.DataAccessExtensions.Commands;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions
{
    /// <summary>
    /// A collection of extension methods on IDbConnection.
    /// </summary>
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, string sprocName)
            where TEntity : class, new()
        {
            return ExecuteSproc<TEntity>(connection, null, sprocName);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            return ExecuteSproc<TEntity>(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, string sprocName,
            params Parameter[] parameters)
            where TEntity : class, new()
        {
            return ExecuteSproc<TEntity>(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC in the given transaction and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, (IDbDataParameter[])null);

            return command.Execute();
        }

        /// <summary>
        /// Executes a stored procedure via RPC in the given transaction and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        /// <summary>
        /// Executes a stored procedure via RPC in the given transaction and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params Parameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, string sprocName)
            where TEntity : class, new()
        {
            return ExecuteSprocAsync<TEntity>(connection, null, sprocName);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            return ExecuteSprocAsync<TEntity>(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, string sprocName,
            params Parameter[] parameters)
            where TEntity : class, new()
        {
            return ExecuteSprocAsync<TEntity>(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName)
                    where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, (IDbDataParameter[])null);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
                    params IDbDataParameter[] parameters)
                    where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params Parameter[] parameters)
            where TEntity : class, new()
        {
            var command = new GetListOfEntitiesCommand<TEntity>(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TColumn> ExecuteSprocSingleColumn<TColumn>(this IDbConnection connection, string sprocName)
        {
            return ExecuteSprocSingleColumn<TColumn>(connection, null, sprocName);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TColumn> ExecuteSprocSingleColumn<TColumn>(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            return ExecuteSprocSingleColumn<TColumn>(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TColumn> ExecuteSprocSingleColumn<TColumn>(this IDbConnection connection, string sprocName,
            params Parameter[] parameters)
        {
            return ExecuteSprocSingleColumn<TColumn>(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC within the given transaction and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TColumn> ExecuteSprocSingleColumn<TColumn>(this IDbConnection connection, IDbTransaction transaction, string sprocName)
        {
            var command = new GetSingleColumnCommand<TColumn>(connection, transaction, sprocName, (IDbDataParameter[])null);

            return command.Execute();
        }

        /// <summary>
        /// Executes a stored procedure via RPC within the given transaction and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TColumn> ExecuteSprocSingleColumn<TColumn>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
                    params IDbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TColumn>(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        /// <summary>
        /// Executes a stored procedure via RPC within the given transaction and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TColumn> ExecuteSprocSingleColumn<TColumn>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params Parameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TColumn>(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TColumn>> ExecuteSprocSingleColumnAsync<TColumn>(this IDbConnection connection, string sprocName)
        {
            return ExecuteSprocSingleColumnAsync<TColumn>(connection, null, sprocName);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TColumn>> ExecuteSprocSingleColumnAsync<TColumn>(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            return ExecuteSprocSingleColumnAsync<TColumn>(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TColumn>> ExecuteSprocSingleColumnAsync<TColumn>(this IDbConnection connection, string sprocName,
            params Parameter[] parameters)
        {
            return ExecuteSprocSingleColumnAsync<TColumn>(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TColumn>> ExecuteSprocSingleColumnAsync<TColumn>(this IDbConnection connection, IDbTransaction transaction, string sprocName)
        {
            var command = new GetSingleColumnCommand<TColumn>(connection, transaction, sprocName, (IDbDataParameter[])null);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TColumn>> ExecuteSprocSingleColumnAsync<TColumn>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TColumn>(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns a list of the first column of results.
        /// </summary>
        /// <typeparam name="TColumn">The type of result to return.</typeparam>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TColumn>> ExecuteSprocSingleColumnAsync<TColumn>(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params Parameter[] parameters)
        {
            var command = new GetSingleColumnCommand<TColumn>(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(this IDbConnection connection, string sprocName)
        {
            return ExecuteSprocNonQuery(connection, null, sprocName);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            return ExecuteSprocNonQuery(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(this IDbConnection connection, string sprocName,
            params Parameter[] parameters)
        {
            return ExecuteSprocNonQuery(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC within the given transaction and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(this IDbConnection connection, IDbTransaction transaction, string sprocName)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, (IDbDataParameter[])null);

            return command.Execute();
        }

        /// <summary>
        /// Executes a stored procedure via RPC within the given transaction and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(this IDbConnection connection, IDbTransaction transaction, string sprocName,
                    params IDbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        /// <summary>
        /// Executes a stored procedure via RPC within the given transaction and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params Parameter[] parameters)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, string sprocName)
        {
            return ExecuteSprocNonQueryAsync(connection, null, sprocName);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            return ExecuteSprocNonQueryAsync(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, string sprocName,
            params Parameter[] parameters)
        {
            return ExecuteSprocNonQueryAsync(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, (IDbDataParameter[])null);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns the number of rows affected.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params Parameter[] parameters)
        {
            var command = new NonQueryCommand(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns the XDocument of the XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, string sprocName)
        {
            return ExecuteSprocXmlScalar(connection, null, sprocName);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the XDocument of the XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            return ExecuteSprocXmlScalar(connection, null, sprocName, parameters);
        }
        
        /// <summary>
        /// Executes a stored procedure via RPC and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the XDocument of the XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, string sprocName,
            params Parameter[] parameters)
        {
            return ExecuteSprocXmlScalar(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC within the given transaction and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns the XDocument of the XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, IDbTransaction transaction, string sprocName)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, (IDbDataParameter[])null);

            return command.Execute();
        }

        /// <summary>
        /// Executes a stored procedure via RPC within the given transaction and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the XDocument of the XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        /// <summary>
        /// Executes a stored procedure via RPC within the given transaction and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the XDocument of the XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params Parameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, parameters);

            return command.Execute();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns the XDocument of the XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, string sprocName)
        {
            return ExecuteSprocXmlScalarAsync(connection, null, sprocName);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the XDocument of the XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, string sprocName,
            params IDbDataParameter[] parameters)
        {
            return ExecuteSprocXmlScalarAsync(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the XDocument of the XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, string sprocName,
            params Parameter[] parameters)
        {
            return ExecuteSprocXmlScalarAsync(connection, null, sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns the XDocument of the XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, (IDbDataParameter[])null);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the XDocument of the XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params IDbDataParameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC within the given transaction and returns the XDocument of the XML value of the first column of the first row.
        /// </summary>
        /// <param name="connection">The IDbConnection connection.</param>
        /// <param name="transaction">The active IDbTransaction, or null.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the XDocument of the XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this IDbConnection connection, IDbTransaction transaction, string sprocName,
            params Parameter[] parameters)
        {
            var command = new GetXDocumentFromScalarCommand(connection, transaction, sprocName, parameters);

            return command.ExecuteAsync();
        }
    }
}
