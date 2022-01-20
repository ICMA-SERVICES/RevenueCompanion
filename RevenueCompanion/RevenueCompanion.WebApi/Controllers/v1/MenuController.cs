using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.MenuSetup;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class MenuController : BaseApiController
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;

        }
        [HttpGet("get-role-pages/{roleId}")]
        [ProducesResponseType(typeof(Response<List<MenuSetupDTO>>), 200)]
        public async Task<IActionResult> GetPagesByRoleId(string roleId)
        {
            var response = await _menuRepository.GetMenu(roleId);
            if (response != null)
                return Ok(new Response<List<MenuSetupDTO>>
                {
                    Data = response,
                    Message = "Menulist  was retrieved successfully ",
                    ResponseCode = ApplicationConstants.SuccessResponseCode,
                    StatusCode = ApplicationConstants.SuccessStatusCode,
                    Succeeded = true
                });
            return Ok(ApplicationConstants.FailureMessage("Failure retrieving Menulist"));
        }
        [AllowAnonymous]
        [HttpGet("get-user-pages/{userId}")]
        [ProducesResponseType(typeof(Response<List<MenuSetupDTO>>), 200)]
        public async Task<IActionResult> GetPagesAssignedToUser(string userId)
        {
            var userPageSecret = Request.Headers["userPageSecret"].ToString();
            var response = await _menuRepository.GetPagesAssignedToUser(userId, userPageSecret);
            if (response != null)
                return Ok(new Response<List<MenuSetupDTO>>
                {
                    Data = response,
                    Message = "Menulist  was retrieved successfully ",
                    ResponseCode = ApplicationConstants.SuccessResponseCode,
                    StatusCode = ApplicationConstants.SuccessStatusCode,
                    Succeeded = true
                });
            return Ok(ApplicationConstants.FailureMessage("Failure retrieving Menulist"));
        }
        [AllowAnonymous]
        [HttpGet("get-approval-menus")]
        [ProducesResponseType(typeof(Response<List<MenuSetupDTO>>), 200)]
        public async Task<IActionResult> GetMenusWithApprovalSettingAsTrue()
        {
            var response = await _menuRepository.GetMenusWithApprovalSettingAsTrue();
            if (response != null)
                return Ok(new Response<List<MenuSetupDTO>>
                {
                    Data = response,
                    Message = "Menulist  was retrieved successfully ",
                    ResponseCode = ApplicationConstants.SuccessResponseCode,
                    StatusCode = ApplicationConstants.SuccessStatusCode,
                    Succeeded = true
                });
            return Ok(ApplicationConstants.FailureMessage("Failure retrieving Menulist"));
        }


        [HttpPost]
        [Route("assign-to-user")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> AssignMenu([FromBody] MenuToUserRequest request)
        {
            if (!(request.Menus.Count() > 0))
            {
                return Ok(ApplicationConstants.FailureMessage("You haven't checked any pages yet!"));
            }
            var response = await _menuRepository.AssignMenusToUser(request);
            if (response > 0)
                return Ok(ApplicationConstants.SuccessMessage("Menu was successfully assign to users"));
            return Ok(ApplicationConstants.FailureMessage("Failure Assigning menu to users"));
        }

        [HttpPost("create-menu")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public IActionResult CreateMenu([FromBody] CreateMenuSetupDTO createMenuSetupDTO)
        {
            var response = _menuRepository.CreateMenuSetup(createMenuSetupDTO);
            if (response.Succeeded)
                return Ok(ApplicationConstants.SuccessMessage("Menu created successfully"));
            return Ok(ApplicationConstants.FailureMessage("Failure creating menu"));
        }

        [HttpPost("remove-menu")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> RemoveAssignMenu([FromBody] MenuToUserRequest request)
        {
            if (!(request.Menus.Count() > 0))
            {
                return Ok(ApplicationConstants.FailureMessage("You haven't checked any pages yet!"));
            }
            var response = await _menuRepository.DeleteMenuAssignedToUser(request);
            if (response > 0)
                return Ok(ApplicationConstants.SuccessMessage("Vat was deleted successfully"));
            return Ok(ApplicationConstants.FailureMessage("Failure deleting Assigned menu to users"));
        }
        [AllowAnonymous]
        [HttpGet("getStateUrlSetup")]
        public async Task<IActionResult> GetStateUrlSetup()
        {
            var browseUrl = Request.Headers["browserUrl"].ToString();
            return Ok(await _menuRepository.GetMerchantSetup(browseUrl));
        }

    }
}
