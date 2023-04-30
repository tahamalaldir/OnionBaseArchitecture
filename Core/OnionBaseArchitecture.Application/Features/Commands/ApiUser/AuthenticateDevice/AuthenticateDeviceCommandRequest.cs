using MediatR;

namespace OnionBaseArchitecture.Application.Features.Commands.ApiUser.AuthenticateDevice
{
    public class AuthenticateDeviceCommandRequest : IRequest<AuthenticateDeviceCommandResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Language { get; set; }
    }
}
