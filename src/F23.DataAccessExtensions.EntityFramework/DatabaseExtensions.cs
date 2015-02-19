using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace F23.DataAccessExtensions
{
    /// <summary>
    /// A collection of extensions on Entity Framework's Database type.
    /// </summary>
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(this Database database, string sprocName, params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            return database.Connection.ExecuteSproc<TEntity>(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(this Database database, string sprocName, params Parameter[] parameters)
            where TEntity : class, new()
        {
            return database.Connection.ExecuteSproc<TEntity>(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this Database database, string sprocName, params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            return database.Connection.ExecuteSprocAsync<TEntity>(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(this Database database, string sprocName, params Parameter[] parameters)
            where TEntity : class, new()
        {
            return database.Connection.ExecuteSprocAsync<TEntity>(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of the values in the first column of results.</returns>
        public static IList<TResult> ExecuteSprocSingleColumn<TResult>(this Database database, string sprocName, params IDbDataParameter[] parameters)
        {
            return database.Connection.ExecuteSprocSingleColumn<TResult>(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of the values in the first column of results.</returns>
        public static IList<TResult> ExecuteSprocSingleColumn<TResult>(this Database database, string sprocName, params Parameter[] parameters)
        {
            return database.Connection.ExecuteSprocSingleColumn<TResult>(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of the values in the first column of results.</returns>
        public static Task<IList<TResult>> ExecuteSprocSingleColumnAsync<TResult>(this Database database, string sprocName, params IDbDataParameter[] parameters)
        {
            return database.Connection.ExecuteSprocSingleColumnAsync<TResult>(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of the values in the first column of results.</returns>
        public static Task<IList<TResult>> ExecuteSprocSingleColumnAsync<TResult>(this Database database, string sprocName, params Parameter[] parameters)
        {
            return database.Connection.ExecuteSprocSingleColumnAsync<TResult>(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(this Database database, string sprocName, params IDbDataParameter[] parameters)
        {
            return database.Connection.ExecuteSprocNonQuery(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(this Database database, string sprocName, params Parameter[] parameters)
        {
            return database.Connection.ExecuteSprocNonQuery(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(this Database database, string sprocName, params IDbDataParameter[] parameters)
        {
            return database.Connection.ExecuteSprocNonQueryAsync(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(this Database database, string sprocName, params Parameter[] parameters)
        {
            return database.Connection.ExecuteSprocNonQueryAsync(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(this Database database, string sprocName, params IDbDataParameter[] parameters)
        {
            return database.Connection.ExecuteSprocXmlScalar(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(this Database database, string sprocName, params Parameter[] parameters)
        {
            return database.Connection.ExecuteSprocXmlScalar(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this Database database, string sprocName, params IDbDataParameter[] parameters)
        {
            return database.Connection.ExecuteSprocXmlScalarAsync(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(this Database database, string sprocName, params Parameter[] parameters)
        {
            return database.Connection.ExecuteSprocXmlScalarAsync(database.GetUnderlyingTransaction(), sprocName, parameters);
        }

        /// <summary>
        /// Gets the underlying IDbTransaction (if any) of the Entity Framework Database instance.
        /// </summary>
        /// <param name="database">The Entity Framework Database instance.</param>
        /// <returns>Returns the underlying IDbTransaction if any, otherwise null.</returns>
        public static IDbTransaction GetUnderlyingTransaction(this Database database)
        {
            if (database == null || database.CurrentTransaction == null)
                return null;

            return database.CurrentTransaction.UnderlyingTransaction;
        }
    }
}
