using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions.Commands
{
    /// <summary>
    /// A base class for creating custom "commands" that can operate on the results of a stored procedure execution.
    /// </summary>
    /// <typeparam name="TCommandResult">The type of result returned from the command.</typeparam>
    public abstract class StoredProcedureCommandBase<TCommandResult> : IStoredProcedureCommand<TCommandResult>
    {
        private readonly IDbConnection _connection;
        private readonly DbConnection _dbConnectionForAsync;
        private readonly IDbTransaction _transaction;
        private readonly string _storedProcedureName;
        private readonly IEnumerable<IDbDataParameter> _parameters;
        private readonly IEnumerable<DbDataParameter> _deferredParameters;

        private readonly bool _useDeferredParameters;

        /// <summary>
        /// Creates a new StoredProcedureCommandBase-derived instance.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="transaction">The active database transaction, if any.</param>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">The parameters to pass to the stored procedure.</param>
        protected StoredProcedureCommandBase(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<IDbDataParameter> parameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (storedProcedureName == null) throw new ArgumentNullException("storedProcedureName");

            _connection = connection;
            _dbConnectionForAsync = connection as DbConnection;
            _transaction = transaction;
            _storedProcedureName = storedProcedureName;
            _parameters = parameters ?? new IDbDataParameter[0];

            _useDeferredParameters = false;
        }

        /// <summary>
        /// Creates a new StoredProcedureCommandBase-derived instance.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="transaction">The active database transaction, if any.</param>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">The parameters to pass to the stored procedure.</param>
        protected StoredProcedureCommandBase(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<DbDataParameter> parameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (storedProcedureName == null) throw new ArgumentNullException("storedProcedureName");

            _connection = connection;
            _dbConnectionForAsync = connection as DbConnection;
            _transaction = transaction;
            _storedProcedureName = storedProcedureName;
            _deferredParameters = parameters ?? new DbDataParameter[0];

            _useDeferredParameters = true;
        }

        /// <summary>
        /// Executes the command and returns the result.
        /// </summary>
        /// <returns>Returns the result of the stored procedure.</returns>
        public TCommandResult Execute()
        {
            bool needsToClose = false;

            try
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                    needsToClose = true;
                }

                var dbCommand = PrepareDbCommand();

                return ExecuteInternal(dbCommand);
            }
            finally
            {
                if (needsToClose)
                    _connection.Close();
            }
        }

        /// <summary>
        /// Asynchronously executes the command and returns the result.
        /// </summary>
        /// <returns>Asynchronously returns the result of the stored procedure.</returns>
        public async Task<TCommandResult> ExecuteAsync()
        {
            bool needsToClose = false;

            try
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    if (_dbConnectionForAsync != null)
                        await _dbConnectionForAsync.OpenAsync();
                    else
                    {
                        Debug.WriteLine("Warning: Current IDbConnection does not support OpenAsync, opening connection synchronously.");
                        _connection.Open();
                    }

                    needsToClose = true;
                }

                var dbCommand = PrepareDbCommand();

                var dbCommandForAsync = dbCommand as DbCommand;

                if (dbCommandForAsync != null)
                    return await ExecuteInternalAsync(dbCommandForAsync);
                else
                {
                    Debug.WriteLine("Warning: Current IDbConnection does not create a command that supports async, running command synchronously.");
                    return ExecuteInternal(dbCommand);
                }
            }
            finally
            {
                if (needsToClose)
                    _connection.Close();
            }
        }

        /// <summary>
        /// Does something with the prepared IDbCommand for the stored procedure.
        /// </summary>
        /// <param name="dbCommand">The IDbCommand prepared for execution.</param>
        /// <returns>Returns the result of the stored procedure.</returns>
        protected internal abstract TCommandResult ExecuteInternal(IDbCommand dbCommand);
        
        /// <summary>
        /// Asynchronously does something with the prepared IDbCommand for the stored procedure.
        /// </summary>
        /// <param name="dbCommand">The IDbCommand prepared for execution.</param>
        /// <returns>Asynchronously returns the result of the stored procedure.</returns>
        protected internal abstract Task<TCommandResult> ExecuteInternalAsync(DbCommand dbCommand);

        private IDbCommand PrepareDbCommand()
        {
            var command = _connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = _storedProcedureName;

            if (_useDeferredParameters)
            {
                AddDeferredParametersToCommand(command);
            }
            else
            {
                AddParametersToCommand(command);
            }

            if (_transaction != null)
                command.Transaction = _transaction;

            return command;
        }

        private void AddParametersToCommand(IDbCommand command)
        {
            foreach (var parameter in _parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        private void AddDeferredParametersToCommand(IDbCommand command)
        {
            foreach (var deferredParameter in _deferredParameters)
            {
                var parameter = deferredParameter.GetParameter(command);

                command.Parameters.Add(parameter);
            }
        }
    }
}
