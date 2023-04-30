using MediatR;
using OnionBaseArchitecture.Application.Abstractions.Services;

namespace OnionBaseArchitecture.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            Tuple<bool, string> res = await _userService.CreateUserAsync(request.Email, request.Username, request.Name, request.Surname, request.PhoneNumber, request.Password);

            return new CreateUserCommandResponse
            {
                Success = res.Item1,
                Message = res.Item2
            };
        }
    }
}
