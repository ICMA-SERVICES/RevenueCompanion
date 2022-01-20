using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueCompanion.Application.Features.App.Commands;
using RevenueCompanion.Application.Features.App.Queries;
using RevenueCompanion.Application.Helpers;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "GlobalAdmin")]
    public class AppController : BaseApiController
    {
        private readonly IAppRepository _appRepository;

        public AppController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }


        [HttpPost]
        [Route("create-new")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> CreateApp(CreateAppCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> UpdateApp(UpdateAppCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [Route("delete")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> DeleteApp(DeleteAppCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAllCategory(DataSourceLoadOptions loadOptions)
        {
            var result = _appRepository.GetApps();
            loadOptions.PrimaryKey = new[] { $"AppId" };
            return Ok(DataSourceLoader.Load(result.OrderBy(x => x.CreatedOn), loadOptions));
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> GetApp(int id)
        {
            return Ok(await Mediator.Send(new GetAppByIdQuery { AppId = id }));
        }
    }
}
