using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionBaseArchitecture.Application.Abstractions.Caching;
using OnionBaseArchitecture.Application.Features.Commands.User.LoginUser;

namespace OnionBaseArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IMediator _mediator;

        public AuthController(ICacheService cacheService, IMediator mediator)
        {
            _cacheService = cacheService;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<string> Get()
        {
            return await _cacheService.StringControl("test");
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
