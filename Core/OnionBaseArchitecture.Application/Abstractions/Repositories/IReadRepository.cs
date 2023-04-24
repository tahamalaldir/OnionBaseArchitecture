using Dapper;
using OnionBaseArchitecture.Domain.Entities.Common;
using System.Data;

namespace OnionBaseArchitecture.Application.Abstractions.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> QeryAsync(string sql, DynamicParameters parameters, CommandType commandType = CommandType.Text);

        Task<T> QeryFirstAsync(string sql, DynamicParameters parameters, CommandType commandType = CommandType.Text);

        Task<T> GetByIdAsync(string Id);

        Task<T> GetByIdActiveAsync(string Id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllActiveAsync();

        Task<int> ExecuteAsync(string sql, DynamicParameters parameters, CommandType commandType = CommandType.Text);
    }
}
