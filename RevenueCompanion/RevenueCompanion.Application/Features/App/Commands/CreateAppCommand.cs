using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.App;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.App.Commands
{
    public class CreateAppCommand : IRequest<Response<string>>
    {
        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2, ErrorMessage = "We only accept to letters for app code")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MenuUrl { get; set; }

    }

    public class CreateAppCommandHandler : IRequestHandler<CreateAppCommand, Response<string>>
    {
        private readonly IAppRepository _appRepository;

        public CreateAppCommandHandler(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        public async Task<Response<string>> Handle(CreateAppCommand request, CancellationToken cancellationToken)
        {
            var response = _appRepository.CreateApp(new CreateAppDTO
            {
                Code = request.Code,
                MenuUrl = request.MenuUrl,
                Name = request.Name
            });

            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("app created successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }
}
