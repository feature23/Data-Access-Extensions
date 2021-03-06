﻿using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace F23.DataAccessExtensions
{
    /// <summary>
    /// A collection of extensions on Entity Framework's DbContext type.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Uses SqlBulkCopy to provide a high-performance bulk insert into a SQL Server database table.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="tableName">The name of the destination SQL Server database table</param>
        /// <param name="source">The items to insert</param>
        /// <typeparam name="TEntity">The type of the items to insert</typeparam>
        public static void BulkInsert<TEntity>(this DbContext context, string tableName, IEnumerable<TEntity> source)
             => context.Database.BulkInsert(tableName, source);

        /// <summary>
        /// Asynchronously uses SqlBulkCopy to provide a high-performance bulk insert into a SQL Server database table.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="tableName">The name of the destination SQL Server database table</param>
        /// <param name="source">The items to insert</param>
        /// <typeparam name="TEntity">The type of the items to insert</typeparam>
        /// <returns>Asynchronously returns</returns>
        public static async Task BulkInsertAsync<TEntity>(this DbContext context, string tableName, IEnumerable<TEntity> source)
            => await context.Database.BulkInsertAsync(tableName, source);

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(DbContext context, string sprocName)
            where TEntity : class, new()
        {
            return context.Database.ExecuteSproc<TEntity>(sprocName);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(DbContext context, string sprocName, params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            return context.Database.ExecuteSproc<TEntity>(sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of results.</returns>
        public static IList<TEntity> ExecuteSproc<TEntity>(DbContext context, string sprocName, params Parameter[] parameters)
            where TEntity : class, new()
        {
            return context.Database.ExecuteSproc<TEntity>(sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(DbContext context, string sprocName)
            where TEntity : class, new()
        {
            return context.Database.ExecuteSprocAsync<TEntity>(sprocName);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(DbContext context, string sprocName, params IDbDataParameter[] parameters)
            where TEntity : class, new()
        {
            return context.Database.ExecuteSprocAsync<TEntity>(sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list of results.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of results.</returns>
        public static Task<IList<TEntity>> ExecuteSprocAsync<TEntity>(DbContext context, string sprocName, params Parameter[] parameters)
            where TEntity : class, new()
        {
            return context.Database.ExecuteSprocAsync<TEntity>(sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns a list of the values in the first column of results.</returns>
        public static IList<TResult> ExecuteSprocSingleColumn<TResult>(DbContext context, string sprocName)
        {
            return context.Database.ExecuteSprocSingleColumn<TResult>(sprocName);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of the values in the first column of results.</returns>
        public static IList<TResult> ExecuteSprocSingleColumn<TResult>(DbContext context, string sprocName, params IDbDataParameter[] parameters)
        {
            return context.Database.ExecuteSprocSingleColumn<TResult>(sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns a list of the values in the first column of results.</returns>
        public static IList<TResult> ExecuteSprocSingleColumn<TResult>(DbContext context, string sprocName, params Parameter[] parameters)
        {
            return context.Database.ExecuteSprocSingleColumn<TResult>(sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns a list of the values in the first column of results.</returns>
        public static Task<IList<TResult>> ExecuteSprocSingleColumnAsync<TResult>(DbContext context, string sprocName)
        {
            return context.Database.ExecuteSprocSingleColumnAsync<TResult>(sprocName);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of the values in the first column of results.</returns>
        public static Task<IList<TResult>> ExecuteSprocSingleColumnAsync<TResult>(DbContext context, string sprocName, params IDbDataParameter[] parameters)
        {
            return context.Database.ExecuteSprocSingleColumnAsync<TResult>(sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns a list with the first column of results.
        /// </summary>
        /// <typeparam name="TResult">The type of the first column of results.</typeparam>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns a list of the values in the first column of results.</returns>
        public static Task<IList<TResult>> ExecuteSprocSingleColumnAsync<TResult>(DbContext context, string sprocName, params Parameter[] parameters)
        {
            return context.Database.ExecuteSprocSingleColumnAsync<TResult>(sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(DbContext context, string sprocName)
        {
            return context.Database.ExecuteSprocNonQuery(sprocName);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(DbContext context, string sprocName, params IDbDataParameter[] parameters)
        {
            return context.Database.ExecuteSprocNonQuery(sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteSprocNonQuery(DbContext context, string sprocName, params Parameter[] parameters)
        {
            return context.Database.ExecuteSprocNonQuery(sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(DbContext context, string sprocName)
        {
            return context.Database.ExecuteSprocNonQueryAsync(sprocName);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(DbContext context, string sprocName, params IDbDataParameter[] parameters)
        {
            return context.Database.ExecuteSprocNonQueryAsync(sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the number of rows affected.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the number of rows affected.</returns>
        public static Task<int> ExecuteSprocNonQueryAsync(DbContext context, string sprocName, params Parameter[] parameters)
        {
            return context.Database.ExecuteSprocNonQueryAsync(sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(DbContext context, string sprocName)
        {
            return context.Database.ExecuteSprocXmlScalar(sprocName);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(DbContext context, string sprocName, params IDbDataParameter[] parameters)
        {
            return context.Database.ExecuteSprocXmlScalar(sprocName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static XDocument ExecuteSprocXmlScalar(DbContext context, string sprocName, params Parameter[] parameters)
        {
            return context.Database.ExecuteSprocXmlScalar(sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <returns>Asynchronously returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(DbContext context, string sprocName)
        {
            return context.Database.ExecuteSprocXmlScalarAsync(sprocName);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(DbContext context, string sprocName, params IDbDataParameter[] parameters)
        {
            return context.Database.ExecuteSprocXmlScalarAsync(sprocName, parameters);
        }

        /// <summary>
        /// Asynchronously executes a stored procedure via RPC and returns the SQL XML document of the first column of the first row returned.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        /// <param name="sprocName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Parameters to pass to the stored procedure.</param>
        /// <returns>Asynchronously returns the XDocument of the SQL XML value of the first column of the first row.</returns>
        public static Task<XDocument> ExecuteSprocXmlScalarAsync(DbContext context, string sprocName, params Parameter[] parameters)
        {
            return context.Database.ExecuteSprocXmlScalarAsync(sprocName, parameters);
        }
    }
}
