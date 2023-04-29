using OnionBaseArchitecture.Application.Common;
using OnionBaseArchitecture.Application.DTOs;

namespace OnionBaseArchitecture.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandResponse : BaseResponseModel
    {
        public Token Token { get; set; }
    }
}
