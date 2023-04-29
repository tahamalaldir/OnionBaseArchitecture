using OnionBaseArchitecture.Application.Abstractions.Repositories.User;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly ISettingService _settingService;

        public UserService(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, ISettingService settingService)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _settingService = settingService;
        }

        public async Task<User> GetByUsernameOrEmailAndPassword(string UsernameOrEmail, string Password)
        {
            return await _userReadRepository.GetUsernameOrEmailAndPassword(UsernameOrEmail, Password);
        }

        public async Task<bool> UpdateUserRefreshTokenByUserId(string UserId, string RefreshToken)
        {
            User user = await _userReadRepository.GetByIdActiveAsync(UserId);
            if (user != null)
            {

                int.TryParse(await _settingService.GetSettingBySystemNameAsync("api.token.expireminute"), out int expireMinute);
                user.Token = RefreshToken;
                user.TokenExpireDate = DateTime.UtcNow.AddMinutes(expireMinute * 4);

                return await _userWriteRepository.UpdateAsync(user);
            }

            return false;
        }
    }
}
