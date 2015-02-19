using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace F23.DataAccessExtensions.Commands
{
    internal sealed class GetListOfEntitiesCommand<TEntity> : StoredProcedureCommandBase<IList<TEntity>>
        where TEntity : class, new()
    {
        public GetListOfEntitiesCommand(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<IDbDataParameter> parameters) 
            : base(connection, transaction, storedProcedureName, parameters)
        {
        }

        public GetListOfEntitiesCommand(IDbConnection connection, IDbTransaction transaction, string storedProcedureName, IEnumerable<DbDataParameter> parameters)
            : base(connection, transaction, storedProcedureName, parameters)
        {
        }

        protected internal override IList<TEntity> ExecuteInternal(IDbCommand dbCommand)
        {
            var result = new List<TEntity>();

            using (var reader = dbCommand.ExecuteReader())
            {
                var valueProvider = new DataReaderValueProvider(reader);

                var objectFactory = EntityFactoryFactory.CreateEntityFactory<TEntity>();

                while (reader.Read())
                {
                    var item = objectFactory(valueProvider);

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
                var valueProvider = new DataReaderValueProvider(reader);

                var objectFactory = EntityFactoryFactory.CreateEntityFactory<TEntity>();

                while (await reader.ReadAsync())
                {
                    var item = objectFactory(valueProvider);

                    result.Add(item);
                }

                reader.Close();
            }

            return result;
        }
    }
}
