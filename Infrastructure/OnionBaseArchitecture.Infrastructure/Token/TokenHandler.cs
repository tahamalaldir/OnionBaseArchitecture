using Microsoft.IdentityModel.Tokens;
using OnionBaseArchitecture.Application.Abstractions.Services;
using OnionBaseArchitecture.Application.Abstractions.Token;
using OnionBaseArchitecture.Application.Common;
using OnionBaseArchitecture.Application.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnionBaseArchitecture.Infrastructure.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ISettingService _settingService;
        private readonly JwtConfigs _jwtConfigs;
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;

        public TokenHandler(ISettingService settingService, JwtConfigs jwtConfigs, IUserService userService, AppSettings appSettings)
        {
            this._settingService = settingService;
            _jwtConfigs = jwtConfigs;
            _userService = userService;
            _appSettings = appSettings;
        }

        public async Task<Application.DTOs.Token> CreateTokenAsync(string UserId)
        {
            int.TryParse(await _settingService.GetSettingBySystemNameAsync("api.token.expireminute"), out int expireMinute);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfigs.TokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("uId", UserId.ToEncrypt(_appSettings.EncriptionKey,_appSettings.SaltBase))
                }),
                Expires = DateTime.UtcNow.AddMinutes(expireMinute),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string RefreshToken = CreateRefreshToken();

            await _userService.UpdateUserRefreshTokenByUserId(UserId, RefreshToken);

            return new Application.DTOs.Token
            {
                AccessToken = tokenHandler.WriteToken(token),
                Expiration = DateTime.UtcNow.AddMinutes(expireMinute),
                RefreshToken = RefreshToken
            };
        }

        private string CreateRefreshToken()
        {
            byte[] number = new byte[64];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
