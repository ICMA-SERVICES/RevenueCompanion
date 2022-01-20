using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;
namespace RevenueCompanion.Application.Features.AppUser.Commands
{
    public class DisableAppUserCommand : IRequest<Response<string>>
    {
        public int AppUserId { get; set; }
    }
    public class DisableAppUserCommandHandler : IRequestHandler<DisableAppUserCommand, Response<string>>
    {
        private readonly IAppUserRepository _appUserRepository;

        public DisableAppUserCommandHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task<Response<string>> Handle(DisableAppUserCommand request, CancellationToken cancellationToken)
        {
            var response = _appUserRepository.DisableAppUser(request.AppUserId);
            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("app user disabled successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }
}
