using Microsoft.AspNetCore.Mvc;
using OnionBaseArchitecture.Application.Abstractions.Caching;

namespace OnionBaseArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public AuthController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return await _cacheService.StringControl("test");
        }
    }
}
