using MediatR;
using OnionBaseArchitecture.Application.Abstractions.Services;

namespace OnionBaseArchitecture.Application.Features.Commands.ApiUser.AuthenticateDevice
{
    public class AuthenticateDeviceCommandHandler : IRequestHandler<AuthenticateDeviceCommandRequest, AuthenticateDeviceCommandResponse>
    {
        private readonly IAuthService _authService;

        public AuthenticateDeviceCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthenticateDeviceCommandResponse> Handle(AuthenticateDeviceCommandRequest request, CancellationToken cancellationToken)
        {
            Tuple<bool, string, string> res = await _authService.AuthenticateDeviceAsync(request.Username, request.Password, request.Language);

            return new AuthenticateDeviceCommandResponse
            {
                Success = res.Item1,
                Message = res.Item2,
                Token = res.Item3
            };
        }
    }
}
