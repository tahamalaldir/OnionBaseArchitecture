using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionBaseArchitecture.Application.Features.Commands.User.CreateUser;

namespace OnionBaseArchitecture.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
  {
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [Route("CreateUser")]
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
    {
      CreateUserCommandResponse response = await _mediator.Send(request);

      return Ok(response);
    }

  }
}
