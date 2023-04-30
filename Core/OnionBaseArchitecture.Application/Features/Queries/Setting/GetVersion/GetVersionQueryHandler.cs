using MediatR;
using OnionBaseArchitecture.Application.Abstractions.Services;

namespace OnionBaseArchitecture.Application.Features.Queries.Setting.GetVersion
{
    public class GetVersionQueryHandler : IRequestHandler<GetVersionQueryRequest, GetVersionQueryResponse>
    {
        private readonly ISettingService _settingService;

        public GetVersionQueryHandler(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<GetVersionQueryResponse> Handle(GetVersionQueryRequest request, CancellationToken cancellationToken)
        {
            string Version = await _settingService.GetSettingBySystemNameAsync("mobile.version");

            return new GetVersionQueryResponse
            {
                Success = true,
                Message = "",
                Version = Version
            };
        }
    }
}
