using OnionBaseArchitecture.Application.Abstractions.Caching;
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
        private readonly ICacheService _cacheService;

        public UserService(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, ISettingService settingService, ICacheService cacheService)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _settingService = settingService;
            _cacheService = cacheService;
        }

        public async Task<User> GetByUsernameOrEmailAndPassword(string UsernameOrEmail, string Password)
        {
            return await _userReadRepository.GetUsernameOrEmailAndPassword(UsernameOrEmail, Password);
        }

        public async Task<bool> UpdateUserRefreshTokenByUserId(string UserId, string RefreshToken)
        {
            try
            {
                User user = await _userReadRepository.GetByIdActiveAsync(UserId);
                if (user != null)
                {
                    int.TryParse(await _settingService.GetSettingBySystemNameAsync("api.token.expireminute"), out int expireMinute);
                    user.Token = RefreshToken;
                    user.TokenExpireDate = DateTime.UtcNow.AddMinutes(expireMinute * 4);

                    return await _userWriteRepository.UpdateAsync(user);
                }
            }
            catch (Exception)
            { }

            return false;
        }

        public async Task<Tuple<bool, string>> CreateUserAsync(string Email, string Username, string Name, string Surname, string PhoneNumber, string Password)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(false, "");
            try
            {
                User user = await _userReadRepository.GetUsernameOrEmail(Username, Email);
                if (user == null)
                {
                    int.TryParse(await _settingService.GetSettingBySystemNameAsync("user.password.expiredate"), out int ExpireDate);

                    user = await _userWriteRepository.InsertAsync(new User
                    {
                        Email = Email,
                        Username = Username,
                        Name = Name,
                        Surname = Surname,
                        PhoneNumber = PhoneNumber,
                        Password = Password,
                        PasswordExpireDate = DateTime.UtcNow.AddDays(ExpireDate)
                    });

                    if (user != null)
                        result = new Tuple<bool, string>(true, await _cacheService.StringControl("UserService.CreateUser.Successful"));
                    else
                        result = new Tuple<bool, string>(false, await _cacheService.StringControl("UserService.CreateUser.Unsuccessful"));
                }
                else
                    result = new Tuple<bool, string>(false, await _cacheService.StringControl("UserService.CreateUser.Available"));
            }
            catch (Exception)
            {
                result = new Tuple<bool, string>(false, await _cacheService.StringControl("UnexpectedError"));
            }

            return result;
        }
    }
}
