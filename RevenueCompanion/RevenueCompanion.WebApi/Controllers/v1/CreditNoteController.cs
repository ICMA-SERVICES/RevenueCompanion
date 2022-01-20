using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Features.CreditNote.Commands;
using RevenueCompanion.Application.Features.CreditNote.Queries;
using RevenueCompanion.Application.Helpers;
using RevenueCompanion.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class CreditNoteController : BaseApiController
    {
        [HttpPost]
        [Route("approve")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> Approve(ApproveCreditNoteRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [Route("add-new")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> AddCreditNote(AddCreditNoteRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [Route("disapprove")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> DisApprove(DisapproveCreditNoteRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("get-assessment")]
        [ProducesResponseType(typeof(Response<SearchFromAssessmentDTO>), 200)]
        public async Task<IActionResult> GetAssessment()
        {
            var assessmentRef = Request.Headers["assessmentRef"];
            return Ok(await Mediator.Send(new SearchByAssessmentNoQuery { AssessmentRefNo = assessmentRef }));
        }

        [HttpGet("search-by-paymentref")]
        [ProducesResponseType(typeof(Response<SearchFromReconciliationDTO>), 200)]
        public async Task<IActionResult> SearchByPaymentRef(DataSourceLoadOptions loadOptions)
        {
            var paymentRef = Request.Headers["paymentRef"];
            var responseList = new List<SearchFromReconciliationDTO>();
            var searchResult = await Mediator.Send(new SearchByPaymentRefQuery { PaymentRefNo = paymentRef });
            if (searchResult.Succeeded)
            {
                responseList.Add(searchResult.Data);

                loadOptions.PrimaryKey = new[] { $"PaymentRefNumber" };
                var test = DataSourceLoader.Load(responseList, loadOptions);
                var testtest = JsonSerializer.Serialize(test);
                if (test.data != null)
                {
                    return Ok(test);
                }
                return Ok(new { ResonseList = test, SearchResult = searchResult });
            }
            else
            {
                return Ok(new { ResonseList = responseList, SearchResult = searchResult });
            }


        }

        [HttpGet("get-all-by-userId")]
        [ProducesResponseType(typeof(Response<List<CreditNoteRequestDTO>>), 200)]
        public async Task<IActionResult> GetCreditNoteRequestListByUserId(DataSourceLoadOptions loadOptions)
        {
            var userId = Request.Headers["userId"];
            var response = await Mediator.Send(new GetCreditNoteRequestListQuery { UserId = userId });
            loadOptions.PrimaryKey = new[] { $"CreditNoteRequestId" };
            return Ok(DataSourceLoader.Load(response.Data.OrderByDescending(x => x.DateRequested), loadOptions));
        }

        [HttpGet("get-credit-notes-unattended")]
        [ProducesResponseType(typeof(Response<List<CreditNoteRequestDTO>>), 200)]
        public async Task<IActionResult> GetCreditNoteRequestListNotAttendedToByUserId()
        {
            string userId = Request.Headers["userId"].ToString();
            if (userId is null)
                return BadRequest();
            return Ok(await Mediator.Send(new GetCreditNoteRequestsNotAttendedToByUserQuery { UserId = userId }));
        }

        [HttpGet("get-approval-details/{creditRequestId}")]
        [ProducesResponseType(typeof(Response<List<CreditNoteRequestDTO>>), 200)]
        public async Task<IActionResult> GetCreditNoteRequestApprovalDetails(int creditRequestId)
        {
            return Ok(await Mediator.Send(new GetCreditNoteRequestDetailsByCreditRequestIdQuery { CreditRequestId = creditRequestId }));
        }









        [HttpGet("get-creditnote-by-userId/{userId}")]
        [ProducesResponseType(typeof(Response<List<CreditNoteRequestDTO>>), 200)]
        public async Task<IActionResult> GetCreditNoteRequestListByUserId(string userId)
        {
            var response = await Mediator.Send(new GetCreditNoteRequestListQuery { UserId = userId });
            return Ok(response);
        }

        [HttpGet("get-credit-note-list-unattended/{userId}")]
        [ProducesResponseType(typeof(Response<List<CreditNoteRequestDTO>>), 200)]
        public async Task<IActionResult> GetCreditNoteRequestListNotAttendedByThisUser(string userId)
        {
            if (userId is null)
                return BadRequest();
            return Ok(await Mediator.Send(new GetCreditNoteRequestsNotAttendedToByUserQuery { UserId = userId }));
        }


    }
}
