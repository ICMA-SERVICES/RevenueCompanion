using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.App;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;
namespace RevenueCompanion.Application.Features.App.Queries
{
    public class GetAppByIdQuery : IRequest<Response<AppDTO>>
    {
        public int AppId { get; set; }
    }
    public class GetAppByIdQueryHandler : IRequestHandler<GetAppByIdQuery, Response<AppDTO>>
    {
        private readonly IAppRepository _appRepository;

        public GetAppByIdQueryHandler(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public async Task<Response<AppDTO>> Handle(GetAppByIdQuery request, CancellationToken cancellationToken)
        {
            var response = _appRepository.GetApp(request.AppId);
            if (response is null)
                return ApplicationConstants.FailureMessage<AppDTO>(response, "failed to retrieve app");
            return ApplicationConstants.SuccessMessage<AppDTO>(response, "success");
        }
    }
}
