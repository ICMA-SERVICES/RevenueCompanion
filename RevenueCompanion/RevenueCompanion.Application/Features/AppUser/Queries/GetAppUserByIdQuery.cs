using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.AppUser;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.AppUser.Queries
{
    public class GetAppUserByIdQuery : IRequest<Response<AppUserDTO>>
    {
        public int AppUserId { get; set; }
    }
    public class GetAppUserByIdQueryHandler : IRequestHandler<GetAppUserByIdQuery, Response<AppUserDTO>>
    {
        private readonly IAppUserRepository _appUserRepository;
        public GetAppUserByIdQueryHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task<Response<AppUserDTO>> Handle(GetAppUserByIdQuery request, CancellationToken cancellationToken)
        {
            var response = _appUserRepository.GetAppUser(request.AppUserId);
            if (response is null)
                return ApplicationConstants.FailureMessage<AppUserDTO>(response, "failed to retrieve app");
            return ApplicationConstants.SuccessMessage<AppUserDTO>(response, "success");
        }
    }


}
