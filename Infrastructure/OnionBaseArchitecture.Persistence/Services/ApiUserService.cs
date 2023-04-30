using OnionBaseArchitecture.Application.Abstractions.Repositories.ApiUser;
using OnionBaseArchitecture.Application.Abstractions.Services;

namespace OnionBaseArchitecture.Persistence.Services
{
    public class ApiUserService : IApiUserService
    {
        private readonly IApiUserReadRepository _repository;

        public ApiUserService(IApiUserReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckApiUser(string Username, string Password)
        {
            return await _repository.GetByUsernameAndPasswordAsync(Username, Password) != null;
        }
    }
}
