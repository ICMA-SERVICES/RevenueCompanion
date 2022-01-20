using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.AppUser;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.AppUser.Commands
{
    public class CreateAppUserCommand : IRequest<Response<string>>
    {
        [Required]
        public string AppCode { get; set; }
        [Required]
        public string UserId { get; set; }
    }
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, Response<string>>
    {
        private readonly IAppUserRepository _appUserRepository;

        public CreateAppUserCommandHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task<Response<string>> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var response = _appUserRepository.CreateAppUser(new CreateAppUserDTO
            {
                AppCode = request.AppCode,
                UserId = request.UserId
            });
            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("app user created successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }
}
