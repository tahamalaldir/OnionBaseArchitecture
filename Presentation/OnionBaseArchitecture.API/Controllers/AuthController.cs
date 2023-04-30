using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionBaseArchitecture.Application.Features.Commands.ApiUser.AuthenticateDevice;
using OnionBaseArchitecture.Application.Features.Commands.User.LoginUser;

namespace OnionBaseArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [Route("AuthenticateDevice")]
        [HttpPost]
        public async Task<IActionResult> AuthenticateDevice(AuthenticateDeviceCommandRequest request)
        {
            AuthenticateDeviceCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }

    }
}
