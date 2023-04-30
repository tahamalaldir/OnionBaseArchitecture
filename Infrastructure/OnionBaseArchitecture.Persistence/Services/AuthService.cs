using OnionBaseArchitecture.Application.Abstractions.Caching;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Application.Abstractions.Token;
using OnionBaseArchitecture.Application.DTOs;
using OnionBaseArchitecture.Domain.Entities;

namespace OnionBaseArchitecture.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IApiUserService _apiUserService;

        public AuthService(IUserService userService, ICacheService cacheService, ITokenHandler tokenHandler, IApiUserService apiUserService)
        {
            _userService = userService;
            _cacheService = cacheService;
            _tokenHandler = tokenHandler;
            _apiUserService = apiUserService;
        }

        public async Task<Tuple<bool, string, Token>> LoginAsync(string UsernameOrEmail, string Password)
        {
            Tuple<bool, string, Token> result = new Tuple<bool, string, Token>(false, "", null);

            User user = await _userService.GetByUsernameOrEmailAndPassword(UsernameOrEmail, Password);
            if (user != null)
            {
                if (!user.IsActive)
                    result = new Tuple<bool, string, Token>(false, await _cacheService.StringControl("AuthService.Login.UserNotActive"), null);
                else if (!user.IsApproved)
                    result = new Tuple<bool, string, Token>(false, await _cacheService.StringControl("AuthService.Login.UserNotApproved"), null);
                else
                {
                    Token token = await _tokenHandler.CreateTokenAsync(user.Id.ToString());
                    result = new Tuple<bool, string, Token>(true, await _cacheService.StringControl("AuthService.Login.Successful"), token);
                }
            }
            else
                result = new Tuple<bool, string, Token>(false, await _cacheService.StringControl("AuthService.Login.UserNotFound"), null);

            return result;
        }

        public Task PasswordResetAsnyc(string Email)
        {
            throw new NotImplementedException();
        }

        public Task<string> RefreshTokenLoginAsync(string RefreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> FacebookLoginAsync(string AuthToken, int AccessTokenLifeTime)
        {
            throw new NotImplementedException();
        }

        public Task<string> GoogleLoginAsync(string IdToken, int AccessTokenLifeTime)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<bool, string, string>> AuthenticateDeviceAsync(string Username, string Password, string Language)
        {
            Tuple<bool, string, string> result = new Tuple<bool, string, string>(false, "", "");
            try
            {
                if (await _apiUserService.CheckApiUser(Username, Password))
                {
                    string Token = await _tokenHandler.CreateMobileTokenAsync(Language);
                    if (!string.IsNullOrEmpty(Token))
                    {
                        result = new Tuple<bool, string, string>(true, "", Token);
                    }
                }
            }
            catch (Exception)
            { }

            return result;
        }
    }
}
