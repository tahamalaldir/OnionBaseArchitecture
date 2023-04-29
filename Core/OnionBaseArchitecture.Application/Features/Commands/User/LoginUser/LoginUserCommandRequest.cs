﻿using MediatR;

namespace OnionBaseArchitecture.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string UsernameOrEmail { get; set; }

        public string Password { get; set; }
    }
}
