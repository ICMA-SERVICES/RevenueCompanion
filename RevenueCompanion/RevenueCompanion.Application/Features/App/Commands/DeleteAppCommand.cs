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
    public class DeleteAppCommand : IRequest<Response<string>>
    {
        [Required]
        public int AppId { get; set; }
    }

    public class DeleteAppCommandHandler : IRequestHandler<DeleteAppCommand, Response<string>>
    {
        private readonly IAppRepository _appRepository;

        public DeleteAppCommandHandler(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        public async Task<Response<string>> Handle(DeleteAppCommand request, CancellationToken cancellationToken)
        {
            var response = _appRepository.DeleteApp(new DeleteAppDTO { AppId = request.AppId });
            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("app deleted successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }
}
