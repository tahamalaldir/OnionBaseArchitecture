using OnionBaseArchitecture.Application.Abstractions;
using OnionBaseArchitecture.Application.Abstractions.Repositories.User;

namespace OnionBaseArchitecture.Persistence.Repositories.User
{
    public class UserWriteRepository : WriteRepository<Domain.Entities.User>, IUserWriteRepository
    {
        public UserWriteRepository(IConnectionManager connectionManager) : base(connectionManager)
        {
        }
    }
}
