using OnionBaseArchitecture.Domain.Entities.Common;

namespace OnionBaseArchitecture.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
    }
}
