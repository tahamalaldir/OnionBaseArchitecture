using OnionBaseArchitecture.Application.Common;

namespace OnionBaseArchitecture.Application.Features.Queries.Setting.GetVersion
{
    public class GetVersionQueryResponse : BaseResponseModel
    {
        public string Version { get; set; }
    }
}
