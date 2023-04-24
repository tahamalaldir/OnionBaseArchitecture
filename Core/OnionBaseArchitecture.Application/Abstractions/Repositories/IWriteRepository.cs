using OnionBaseArchitecture.Domain.Entities.Common;

namespace OnionBaseArchitecture.Application.Abstractions.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteByIdAsync(string Id);

        Task<Tuple<bool, string>> BulkInsertAsync(List<T> entities);

        Task<Tuple<bool, string>> BulkUpdateAsync(List<T> entities);
    }
}
