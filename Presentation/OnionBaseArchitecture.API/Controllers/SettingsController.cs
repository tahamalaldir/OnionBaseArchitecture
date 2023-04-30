using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionBaseArchitecture.Application.Features.Queries.Setting.GetVersion;

namespace OnionBaseArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("GetVersion")]
        [HttpGet]
        public async Task<GetVersionQueryResponse> GetVersion(GetVersionQueryRequest request)
        {
            GetVersionQueryResponse response = await _mediator.Send(request);

            return response;
        }
    }
}