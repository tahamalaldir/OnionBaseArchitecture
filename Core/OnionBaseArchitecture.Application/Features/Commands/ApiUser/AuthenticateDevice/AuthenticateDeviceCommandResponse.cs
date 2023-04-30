using OnionBaseArchitecture.Application.Common;

namespace OnionBaseArchitecture.Application.Features.Commands.ApiUser.AuthenticateDevice
{
    public class AuthenticateDeviceCommandResponse : BaseResponseModel
    {
        public string Token { get; set; }
    }
}
