using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueCompanion.Application.Features.AppUser.Commands;
using RevenueCompanion.Application.Features.AppUser.Queries;
using RevenueCompanion.Application.Helpers;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "MerchantAdmin")]
    public class AppUserController : BaseApiController
    {
        private readonly IAppUserRepository _appUserRepository;
        public AppUserController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }


        [HttpPost]
        [Route("create-new")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> CreateAppUser(CreateAppUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        //[HttpPost]
        //[Route("update")]
        //[ProducesResponseType(typeof(Response<string>), 200)]
        //public async Task<IActionResult> UpdateApp(Upd command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}
        [HttpPost]
        [Route("delete")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> DeleteAppUser(DeleteAppUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [Route("enable")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Enable(EnableAppUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [Route("disable")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Disable(DisableAppUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("get-all")]
        public IActionResult GetAll(DataSourceLoadOptions loadOptions)
        {
            var result = _appUserRepository.GetAll();
            loadOptions.PrimaryKey = new[] { $"AppUserId" };
            return Ok(DataSourceLoader.Load(result.OrderBy(x => x.CreatedOn), loadOptions));
        }
        [HttpGet("get-by-appcode")]
        public IActionResult GetAllByAppCode(DataSourceLoadOptions loadOptions)
        {
            var appCode = HttpContext.Request.Headers["appCode"].ToString();
            var result = _appUserRepository.GetAppUsers(appCode);
            loadOptions.PrimaryKey = new[] { $"AppuserId" };
            return Ok(DataSourceLoader.Load(result.OrderBy(x => x.CreatedOn), loadOptions));
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> GetAppUser(int id)
        {
            return Ok(await Mediator.Send(new GetAppUserByIdQuery { AppUserId = id }));
        }
    }
}
