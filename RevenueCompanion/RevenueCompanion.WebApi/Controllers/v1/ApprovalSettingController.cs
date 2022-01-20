using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueCompanion.Application.Features.ApprovalSettings.Commands;
using RevenueCompanion.Application.Features.ApprovalSettings.Queries;
using RevenueCompanion.Application.Helpers;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class ApprovalSettingController : BaseApiController
    {
        private readonly IApprovalSettingRepository _approvalSettingRepository;

        public ApprovalSettingController(IApprovalSettingRepository approvalSettingRepository)
        {
            _approvalSettingRepository = approvalSettingRepository;
        }


        [HttpPost]
        [Route("create-new")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> CreateApprovalSetting(CreateApprovalSettingCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> UpdateApprovalSetting(UpdateApprovalSettingCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [Route("delete")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> DeleteApprovalSetting(DeleteApprovalSettingsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll(DataSourceLoadOptions loadOptions)
        {
            var result = await _approvalSettingRepository.GetApprovalSettings();
            loadOptions.PrimaryKey = new[] { $"ApprovalSettingId" };
            return Ok(DataSourceLoader.Load(result.OrderBy(x => x.CreatedOn), loadOptions));
        }
        [HttpGet]
        [Route("count-all")]
        public async Task<IActionResult> CountAll()
        {
            var result = await _approvalSettingRepository.GetApprovalSettings();
            return Ok(result.Count());
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> GetApprovalSetting(int id)
        {
            return Ok(await Mediator.Send(new GetApprovalSettingByIdQuery { ApprovalSettingId = id }));
        }
    }
}
