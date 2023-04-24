using Dapper;
using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories;
using OnionBaseArchitecture.Domain.Attibutes;
using OnionBaseArchitecture.Domain.Entities.Common;
using System.Data;
using System.Reflection;

namespace OnionBaseArchitecture.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private IConnectionManager _conn;
        private const int _commandTimeout = 60;

        public ReadRepository(IConnectionManager connectionManager)
        {
            _conn = connectionManager;
        }

        public virtual async Task<IEnumerable<T>> QeryAsync(string sql, DynamicParameters parameters, CommandType commandType = CommandType.Text)
        {
            using (var con = _conn.Connection)
            {
                return await con.QueryAsync<T>(sql, parameters, commandType: commandType, commandTimeout: _commandTimeout);
            }
        }

        public virtual async Task<T> QeryFirstAsync(string sql, DynamicParameters parameters, CommandType commandType = CommandType.Text)
        {
            using (var con = _conn.Connection)
            {
                return await con.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType, commandTimeout: _commandTimeout);
            }
        }

        public virtual async Task<T> GetByIdAsync(string Id)
        {
            var tableAttribute = typeof(T).GetCustomAttribute<TableName>();
            var query = $@"Select * from [{tableAttribute.SchemeName}].[{tableAttribute.Name}] with (NOLOCK) where IsDeleted = 0 AND Id = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", Id, DbType.Int32);
            using (var con = _conn.Connection)
            {
                return await con.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.Text);
            }
        }

        public virtual async Task<T> GetByIdActiveAsync(string Id)
        {
            var tableAttribute = typeof(T).GetCustomAttribute<TableName>();
            var query = $@"Select * from [{tableAttribute.SchemeName}].[{tableAttribute.Name}] with (NOLOCK) where IsDeleted = 0 AND IsActive = 1 AND Id = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", Id, DbType.Int32);
            using (var con = _conn.Connection)
            {
                return await con.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.Text);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var tableAttribute = typeof(T).GetCustomAttribute<TableName>();
            var query = $@"Select * from [{tableAttribute.SchemeName}].[{tableAttribute.Name}] with (NOLOCK) where IsDeleted = 0 ORDER BY Id DESC";

            using (var con = _conn.Connection)
            {
                return await con.QueryAsync<T>(query, null, commandType: CommandType.Text);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllActiveAsync()
        {
            var tableAttribute = typeof(T).GetCustomAttribute<TableName>();
            var query = $@"Select * from [{tableAttribute.SchemeName}].[{tableAttribute.Name}] with (NOLOCK) where IsDeleted = 0 AND IsActive = 1 ORDER BY Id DESC";

            using (var con = _conn.Connection)
            {
                return await con.QueryAsync<T>(query, null, commandType: CommandType.Text);
            }
        }

        public virtual async Task<int> ExecuteAsync(string sql, DynamicParameters parameters, CommandType commandType = CommandType.Text)
        {
            using (var con = _conn.Connection)
            {
                return await con.ExecuteAsync(sql, parameters, commandType: commandType, commandTimeout: _commandTimeout);
            }
        }

    }
}
