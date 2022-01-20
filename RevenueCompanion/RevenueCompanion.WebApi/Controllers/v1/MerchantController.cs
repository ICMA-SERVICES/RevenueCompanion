using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.Merchant;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class MerchantController : BaseApiController
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantController(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        [HttpPost]
        [Route("create-new")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> CreateMerchant([FromBody] CreateMerchantDTO request)
        {
            //var origin = Request.Headers["origin"];
            //request.WebUrl = request.WebUrl == null ? origin : request.WebUrl;
            var response = await _merchantRepository.CreateMerchantAsync(request);
            if (response.Succeeded)
                return Ok(ApplicationConstants.SuccessMessage("merchant created successfuly"));
            return Ok(ApplicationConstants.FailureMessage(response.Message));
        }
        [HttpGet]
        [Route("get-merchant")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> GetMerchant()
        {
            var response = await _merchantRepository.GetFirstMerchant();
            if (response != null)
                return Ok(ApplicationConstants.SuccessMessage<MerchantDTO>(response, "merchant retrieved successfuly"));
            return Ok(ApplicationConstants.FailureMessage<MerchantDTO>(null, "Failed to retrieve merchant"));
        }
    }
}
