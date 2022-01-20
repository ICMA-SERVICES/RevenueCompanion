using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.AppUser;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.AppUser.Commands
{
    public class DeleteAppUserCommand : IRequest<Response<string>>
    {
        public int AppUserId { get; set; }
    }
    public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommand, Response<string>>
    {
        private readonly IAppUserRepository _appUserRepository;

        public DeleteAppUserCommandHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task<Response<string>> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
        {
            var response = _appUserRepository.DeleteAppUser(new DeleteAppUserDTO
            {
                AppUserId = request.AppUserId
            });
            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("app user deleted successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }

}
