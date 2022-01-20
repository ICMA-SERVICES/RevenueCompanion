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

    public class UpdateAppCommand : IRequest<Response<string>>
    {
        [Required]
        public int AppId { get; set; }
        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2, ErrorMessage = "We only accept to letters for app code")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MenuUrl { get; set; }

    }

    public class UpdateAppCommandHandler : IRequestHandler<UpdateAppCommand, Response<string>>
    {
        private readonly IAppRepository _appRepository;

        public UpdateAppCommandHandler(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        public async Task<Response<string>> Handle(UpdateAppCommand request, CancellationToken cancellationToken)
        {
            var response = _appRepository.UpdateApp(new UpdateAppDTO
            {
                AppId = request.AppId,
                Code = request.Code,
                MenuUrl = request.MenuUrl,
                Name = request.Name
            });

            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("app updated successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }
}
