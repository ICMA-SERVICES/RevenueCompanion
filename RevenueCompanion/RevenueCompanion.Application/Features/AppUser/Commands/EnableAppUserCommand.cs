using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.AppUser.Commands
{
    public class EnableAppUserCommand : IRequest<Response<string>>
    {
        public int AppUserId { get; set; }
    }
    public class EnableAppUserCommandHandler : IRequestHandler<EnableAppUserCommand, Response<string>>
    {
        private readonly IAppUserRepository _appUserRepository;

        public EnableAppUserCommandHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task<Response<string>> Handle(EnableAppUserCommand request, CancellationToken cancellationToken)
        {
            var response = _appUserRepository.EnableAppUser(request.AppUserId);
            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("app user enabled successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }
}
