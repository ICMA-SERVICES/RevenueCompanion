using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.Account;
using RevenueCompanion.Application.Helpers;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public AccountController(IAccountService accountService, IAuthenticatedUserService authenticatedUserService)
        {
            _accountService = accountService;
            _authenticatedUserService = authenticatedUserService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));

        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {

            return Ok(await _accountService.RegisterAsync(request));
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code, [FromQuery] string password)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(userId, code, password));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {

            return Ok(await _accountService.ForgotPassword(model));
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {

            return Ok(await _accountService.ResetPassword(model));
        }
        [HttpGet("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            return Ok(await _accountService.Logout());
        }
        [HttpGet("get-users")]
        [Authorize]
        public async Task<IActionResult> GetUsers(DataSourceLoadOptions loadOptions)
        {
            var hasHeaderOfRecentlyCreatedNumber = HttpContext.Request.Headers["RecentlyCreated"];
            var response = new List<UserDTO>();
            if ((!string.IsNullOrEmpty(hasHeaderOfRecentlyCreatedNumber)) && hasHeaderOfRecentlyCreatedNumber == "5")
            {
                response = await _accountService.GetUsers(_authenticatedUserService.UserId, int.Parse(hasHeaderOfRecentlyCreatedNumber));
            }
            else
            {
                response = await _accountService.GetUsers(_authenticatedUserService.UserId);
            }

            if (response != null)
            {
                loadOptions.PrimaryKey = new[] { $"userId" };
                return Ok(DataSourceLoader.Load(response.OrderBy(x => x.FirstName), loadOptions));
            }
            return Ok(DataSourceLoader.Load(new List<UserDTO>().OrderBy(x => x.FirstName), loadOptions));
        }
        [HttpPost("DeleteUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            return Ok(await _accountService.RemoveUser(userId));
        }
        [HttpGet("GetUserCount/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserCount(string userId)
        {
            return Ok(await _accountService.GetUserCount(userId));
        }
        [HttpGet("get-roles")]
        [Authorize]
        public async Task<IActionResult> GetRoles()
        {
            //var header = Request.Headers["IsGeneral"].ToString();
            //var isGeneral = bool.Parse(header);

            return Ok(await _accountService.GetRoles());
        }
        [HttpGet("ResendEmail/{email}")]
        [Authorize]
        public async Task<IActionResult> ResendEmail(string email)
        {
            var webUrl = HttpContext.Request.Headers["webUrl"].ToString();
            return Ok(await _accountService.ResendEmail(email, webUrl));
        }
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        [HttpPost]
        [Route("Enable/{userId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Enable(string userId)
        {
            var response = await _accountService.Enable(userId);
            if (response > 0)
                return Ok(ApplicationConstants.SuccessMessage("User was enabled successfully"));
            return Ok(ApplicationConstants.FailureMessage("Failure enabled user"));
        }

        [HttpPost]
        [Route("Disable/{userId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Disable(string userId)
        {
            var response = await _accountService.Disable(userId);
            if (response > 0)
                return Ok(ApplicationConstants.SuccessMessage("User was disabled successfully"));
            return Ok(ApplicationConstants.FailureMessage("Failure disabled user"));
        }

        [HttpPost]
        [Route("UpdateUser")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser model)
        {
            await _accountService.UpdateUser(model);
            return Ok(ApplicationConstants.SuccessMessage("User was updated successfully"));
        }
        //[HttpPost]
        //[Route("hasexpired/{token}")]
        //[ProducesResponseType(typeof(Response<string>), 200)]
        //public IActionResult HasTokenExpired(string token)
        //{
        //    var response = _jwtService.GetExpiryTimestamp(token);
        //    if (DateTime.Now >= response)
        //    {
        //        return Ok(ResponseHelper.FailureMessage("token expired"));
        //    }
        //    else
        //    {
        //        return Ok(ResponseHelper.SuccessMessage("token still active"));
        //    }

        //}


    }


}