using MediatR;
using OnionBaseArchitecture.Application.Abstractions.Services;

namespace OnionBaseArchitecture.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var res = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);

            return new LoginUserCommandResponse
            {
                Success = res.Item1,
                Message = res.Item2,
                Token = res.Item3
            };
        }
    }
}
