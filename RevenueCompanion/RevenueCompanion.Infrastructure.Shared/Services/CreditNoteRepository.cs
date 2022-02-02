using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using RevenueCompanion.Application.DapperServices;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Domain.Common;
using RevenueCompanion.Domain.Entities;
using RevenueCompanion.Infrastructure.Identity.Models;
using RevenueCompanion.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.Infrastructure.Shared.Services
{
    public class CreditNoteRepository : ICreditNoteRepository
    {
        private readonly ILogger<CreditNoteRepository> _logger;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private string constring;
        private readonly IOptions<ConnectionStrings> myconnectionString;
        private readonly IOptions<ExternalLinks> _processCreditNoteUrl;
        private readonly IDapper _dapper;
        private readonly ApplicationDbContext _context;
        private readonly IAuditRepository _auditRepository;
        private readonly AssessmentsContext _assessmentsContext;
        private readonly IReconcileContext _reconcileContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientHelperService _httpClientHelperService;
        private readonly IcmaCollectionContext _icmaCollectionContext;
        public IConfiguration _config { get; }

        public CreditNoteRepository(ILogger<CreditNoteRepository> logger,
                               IAuthenticatedUserService authenticatedUser,
                               IOptions<ConnectionStrings> connectionString,
                               IOptions<ExternalLinks> processCreditNoteUrl,
                               IDapper dapper,
                               ApplicationDbContext context,
                               IAuditRepository auditRepository,
                               AssessmentsContext assessmentsContext,
                               IReconcileContext reconcileContext,
                               UserManager<ApplicationUser> userManager,
                               IHttpClientHelperService httpClientHelperService,
                               IcmaCollectionContext icmaCollectionContext,
                               IConfiguration configuration)
        {
            _logger = logger;
            _authenticatedUser = authenticatedUser;
            myconnectionString = connectionString;
            _processCreditNoteUrl = processCreditNoteUrl;
            _dapper = dapper;
            _context = context;
            _auditRepository = auditRepository;
            _assessmentsContext = assessmentsContext;
            _reconcileContext = reconcileContext;
            _userManager = userManager;
            _httpClientHelperService = httpClientHelperService;
            _icmaCollectionContext = icmaCollectionContext;
            constring = myconnectionString.Value.DefaultConnection;
            _config = configuration;
        }
        /// <summary>
        /// Create new Credit Note Request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddCreditNoteRequest(CreateCreditNoteRequestDTO request)
        {
            try
            {
                //Retrieve Approval setting from Db. We do this to check the count of required approval for the credit note item
                var approvalSetting = await _context.ApprovalSetting.FirstOrDefaultAsync(c => c.MenuSetupId == request.MenuSetupId);
                if (approvalSetting is null)
                {
                    //_logger.LogError(ex.Message, "An error has occured on CreditNoteRepository, AddCreditNoteRequest");
                    return false;
                }

                //Here we created a new instance of CreditNote Request and save the to DB
                var newCreditRequest = new CreditNoteRequest
                {
                    ActualAmount = request.ActualAmount,
                    AmountUsed = request.AmountUsed,
                    ApprovalCount = 0,
                    ApprovalSettingId = approvalSetting.ApprovalSettingId,
                    Balance = request.Balance,
                    CreatedBy = _authenticatedUser.UserId,
                    CreatedOn = DateTime.Now,
                    DateRequested = request.DateRequested,
                    IsApproved = null,
                    MerchantCode = _authenticatedUser.MerchantCode,
                    NoOfRequiredApproval = approvalSetting.NoOfRequiredApproval,
                    PaymentReferenceNumber = request.PaymentReferenceNumber,
                    RequestedById = _authenticatedUser.UserId,
                    RequestedByEmail = _authenticatedUser.Email,
                    RequestedByName = _authenticatedUser.Name,
                    AssessmentReferenceNumber = request.AssessmentReferenceNumber,
                    CreditNoteRequestTypeId = int.Parse(request.TransType)
                };
                await _context.CreditNoteRequest.AddAsync(newCreditRequest);
                await _context.SaveChangesAsync();

                //Create an audit of the completed action
                await _auditRepository.CreateAudit(_authenticatedUser.UserId, $"Created a new credit note request with PaymentRef number {request.PaymentReferenceNumber}");



                //Change the status of the reconcilliation info. set IsUsedByPlatform to Yes
                if (request.TransType == "1")
                {
                    var reconciliationData = await _reconcileContext.Collection.FirstOrDefaultAsync(c => c.PaymentRefNumber == request.PaymentReferenceNumber);
                    if(reconciliationData != null)
                    {
                        reconciliationData.UsedByPlatform = "Yes";
                        _reconcileContext.Collection.Update(reconciliationData);
                        await _reconcileContext.SaveChangesAsync();
                    }

                    var reconciliationDataIcma = await _icmaCollectionContext.CollectionReports.FirstOrDefaultAsync(c => c.PaymentRefNumber == request.PaymentReferenceNumber);
                    if (reconciliationDataIcma != null)
                    {
                        reconciliationDataIcma.UsedByPlatform = "Yes";
                        _icmaCollectionContext.CollectionReports.Update(reconciliationDataIcma);
                        await _reconcileContext.SaveChangesAsync();
                    }

                }


                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured on CreditNoteRepository, AddCreditNoteRequest");
                return false;
            }
        }

        /// <summary>
        /// Approve credit note request
        /// </summary>
        /// <param name="creditRequestId">The Credit Note Request ID to be disapproved </param>
        /// <param name="isApproved"></param>
        /// <param name="comment">User comment on the approval</param>
        /// <returns>True | False indicating the status of the operation</returns>
        public async Task<RepositoryResponseDTO<bool>> ApproveCreditNoteRequest(int creditRequestId, bool isApproved, string comment = "Satisfactorily")
        {
            try
            {
                //before approval, ensure the approval count is within the specified count
                var request = await GetCreditNoteRequestById(creditRequestId);
                if (request.ApprovalCount >= request.NoOfRequiredApproval)
                {
                    //approval request cannot be granted
                    return GetApprovalResponse(400, "Approval cannot be processed, The credit note request has completed its approval life cycle.", false, false);
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
                    //save approval
                    var approverDetails = new CreditNoteRequestApprovalDetail
                    {
                        ActedUponBy = $"{user.FirstName}  {user.LastName}",
                        ApproverUserId = user.Id,
                        Comment = comment,
                        CreatedBy = _authenticatedUser.UserId,
                        CreatedOn = DateTime.Now,
                        CreditNoteRequestId = creditRequestId,
                        IsDeleted = false,
                        IsApproved = isApproved,
                        Status = "Approved",
                    };

                    await _context.CreditNoteRequestApprovalDetails.AddAsync(approverDetails);
                    await _context.SaveChangesAsync();

                    //Create audit
                    await _auditRepository.CreateAudit(_authenticatedUser.UserId, $"Approved a credit note request with payment ref number: {request.PaymentReferenceNumber}");

                    if (approverDetails.CreditNoteRequestApprovalDetailId > 0)
                    {
                        //if successful, increment request approval count by 1
                        request.ApprovalCount += 1;
                        _context.CreditNoteRequest.Update(request);
                        await _context.SaveChangesAsync();
                        //after incrementing by 1, check if it has reach the maximum approval required
                        if (request.ApprovalCount >= request.NoOfRequiredApproval)
                        {
                            //max limit reached
                            request.IsApproved = true;
                            request.DateApproved = DateTime.Now;
                            _context.CreditNoteRequest.Update(request);
                            await _context.SaveChangesAsync();
                            await _auditRepository.CreateAudit(_authenticatedUser.UserId, $"Gave a final approval to a  credit note request with payment ref number: {request.PaymentReferenceNumber} and it has completed its approval life circle");


                            //Will be scheduled as background task.......
                            await PostToMrIsholaEndpoint(creditRequestId, comment);
                            //Post to Mr Ishola
                        }
                        return GetApprovalResponse(200, "Approved successfully.", true, true);
                    }
                    return GetApprovalResponse(400, "An error occured while processing your request, please try again.", false, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured on CreditNoteRepository, ApproveCreditNoteRequest");
                return GetApprovalResponse(400, "An error occured while processing your request, please try again.", false, false);

            }
        }

        private async Task PostToMrIsholaEndpoint(int creditRequestId, string Comment)
        {
            //retrive the credit note from database
            var creditNote = await _context.CreditNoteRequest.FirstOrDefaultAsync(c => c.CreditNoteRequestId == creditRequestId);

            //gets the assessment tied to the credit note
            var assessment = await _assessmentsContext.Assessment.FirstOrDefaultAsync(c => c.AssessmentRefNo == creditNote.AssessmentReferenceNumber);


            //Creates a new instance of model class in preparation to post to mr Isholas endpoint.
            var requestModel = new PostToExternalRepository
            {
                agencyCode = assessment.AgencyCode,
                agencyName = assessment.AgencyName,
                isAssessmentReversed = Convert.ToBoolean(assessment.IsReversed),
                assessmentNo = assessment.AssessmentRefNo,
                paymentRefNumber = creditNote.PaymentReferenceNumber,
                payerName = assessment.PayerName,
                payerUtin = assessment.AgentUtin,
                proposedAmount = Convert.ToInt32(creditNote.AmountUsed),
                revenueCode = assessment.RevenueCode,
                revenueName = assessment.RevenueName,
                platFormName = assessment.Platformcode,
                year = assessment.TaxYear,
                reason = Comment,
                appId = Int32.Parse(_config.GetSection("UserSettings")["AppId"]),
                SecretKey = _config.GetSection("UserSettings")["SecretKey"],
                MerchantCode = _config.GetSection("UserSettings")["MerchantCode"],
                sourceId = creditNote.CreditNoteRequestTypeId,
                source = creditNote.CreditNoteRequestTypeId == 1 ? "Automatic" : "Manual",
                requestedBy = _authenticatedUser.Email,
                approvedBy = _authenticatedUser.Email,
            };

            var SecretKey = _config.GetSection("UserSettings")["SecretKey"];
            var MerchantCode = _config.GetSection("UserSettings")["MerchantCode"];

            var result = await _httpClientHelperService.PostAsync<PostToExternalRepository>($"{_processCreditNoteUrl.Value.ProcessCreditNoteUrl}Revenue/processCreditNotesRequest", requestModel, "", SecretKey, MerchantCode);
        }

        /// <summary>
        /// Disapprove Credit Note Request
        /// </summary>
        /// <param name="creditRequestId">The Credit Note Request ID to be disapproved </param>
        /// <param name="comment">User comment on why the disapproval</param>
        /// <returns>True | False indicating the status of the operation</returns>
        public async Task<RepositoryResponseDTO<bool>> DisApproveCreditNoteRequest(int creditRequestId, string comment)
        {
            try
            {
                //retrive the credit note request by ID
                var request = await GetCreditNoteRequestById(creditRequestId);
                // If the approval count so far is greater or equal to the required approval, the item has been either approved or disapproved as the case may be
                if (request.ApprovalCount >= request.NoOfRequiredApproval || request.IsApproved != true)
                {
                    //approval request cannot be granted
                    return GetApprovalResponse(400, "Disapproval cannot be processed, The credit note request has completed its approval life cycle.", false, false);
                }
                //nex we check the child approval details. if the count of the child details is greated or equal to the required approval. Approval|Disapproval cannot be granted
                else if ((await _context.CreditNoteRequestApprovalDetails.Where(c => c.CreditNoteRequestId == creditRequestId && c.IsApproved).CountAsync() >= request.NoOfRequiredApproval))
                {
                    return GetApprovalResponse(400, "Disapproval cannot be processed, The credit note request has completed its approval life cycle.", false, false);

                }
                else
                {
                    //Grant disapproval, by saving a row in the child approval table
                    var user = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
                    var approvalDetail = new CreditNoteRequestApprovalDetail
                    {
                        IsApproved = false,
                        ActedUponBy = user.Email,
                        ApproverUserId = user.Id,
                        Comment = comment,
                        CreditNoteRequestId = creditRequestId,
                        Status = "Not Approved",
                        CreatedOn = DateTime.Now,
                        CreatedBy = _authenticatedUser.UserId
                    };

                    await _context.CreditNoteRequestApprovalDetails.AddAsync(approvalDetail);
                    var response = await _context.SaveChangesAsync();
                    return GetApprovalResponse(200, "Disapproved successfully.", true, true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured on CreditNoteRepository, DisapprovedCreditNotRequest");
                return GetApprovalResponse(400, "An error occured while processing your request, please try again.", false, false);

            }


        }
        /// <summary>
        /// Gets record from the assessment repository
        /// </summary>
        /// <param name="assessmentRef">Assessment Reference Number</param>
        /// <returns>First Or Default,  record that matches the assessmentRef passed</returns>
        public async Task<SearchFromAssessmentDTO> GetAssessmentInfoByAssessmentRef(string assessmentRef)
        {
            try
            {
                var assessment = await _assessmentsContext.Assessment.Where(c => c.AssessmentRefNo == assessmentRef).Select(c => new SearchFromAssessmentDTO
                {
                    PayerName = c.PayerName,
                    AssessmentRefNo = c.AssessmentRefNo,
                    AgentUtin = c.AgentUtin,
                    Platformcode = c.Platformcode,
                    TotalAmount = c.TotalAmount,
                    Address = c.Address,
                    AgencyCode = c.AgencyCode,
                    AgencyName = c.AgencyName,
                    AmountPaid = c.AmountPaid,
                    AssessmentApprovedBy = c.AssessmentApprovedBy,
                    AssessmentBalance = c.AssessmentBalance,
                    AssessmentCreatedBy = c.AssessmentCreatedBy,
                    AssessmentCreatedDate = c.AssessmentCreatedDate,
                    AssessmentDateApproved = c.AssessmentDateApproved,
                    AssessmentId = c.AssessmentId,
                    DateCreated = c.DateCreated,
                    DatePushtoXpress = c.DatePushtoXpress,
                    DateReversed = c.DateReversed,
                    IsReversed = c.IsReversed,
                    Location = c.Location,
                    MerchantCode = c.MerchantCode,
                    RevenueCode = c.RevenueCode,
                    RevenueName = c.RevenueName,
                    TaxYear = c.TaxYear,

                }).FirstOrDefaultAsync();
                return assessment;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message, $"No assesment was found for {assessmentRef}, CreditNoteRequestRepository,GetAssessmentInfoByAssessmentRef");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return null;
            }

        }

        /// <summary>
        /// Get a list of Credit Note Request by userId
        /// </summary>
        /// <param name="userId">Current logged in userId</param>
        /// <returns>List of CreditNoteRequestDTO</returns>
        public async Task<List<CreditNoteRequestDTO>> GetCreditNoteRequestListByUserId(string userId)
        {
            try
            {
                var requests = await _context.CreditNoteRequest
               .Where(c => c.RequestedById == userId)
               .OrderByDescending(c => c.DateRequested).Select(c => new CreditNoteRequestDTO
               {
                   ActualAmount = c.ActualAmount,
                   AmountUsed = c.AmountUsed,
                   ApprovalCount = c.ApprovalCount,
                   ApprovalSettingId = c.ApprovalSettingId,
                   DateRequested = c.DateRequested,
                   Balance = c.Balance,
                   CreditNoteRequestId = c.CreditNoteRequestId,
                   DateApproved = c.DateApproved,
                   IsApproved = c.IsApproved,
                   MerchantCode = c.MerchantCode,
                   NoOfRequiredApproval = c.NoOfRequiredApproval,
                   PaymentReferenceNumber = c.PaymentReferenceNumber,
                   RequestedBy = c.RequestedByName,
                   AssessmentReferenceNumber = c.AssessmentReferenceNumber
               }).ToListAsync();
                return requests;
            }
            catch (Exception ex)
            {
                return new List<CreditNoteRequestDTO>();
            }
        }

        /// <summary>
        /// Get a list of child approval details by their master ID
        /// </summary>
        /// <param name="creditRequestId">Credit Note Request ID</param>
        /// <returns>List of CreditNoteRequestDetailsDTO</returns>
        public async Task<CreditNoteDetail> GetCreditNoteRequestDetailsByCreditRequestId(int creditRequestId)
        {

            try
            {
                //get all the approvals so far on the credit note request
                var approvalList = await _context.CreditNoteRequestApprovalDetails
                                    .Where(c => c.CreditNoteRequestId == creditRequestId)
                                    .Select(c => new CreditNoteRequestApprovalDetailDTO
                                    {
                                        ActedUponBy = c.ActedUponBy,
                                        ApproverUserId = c.ApproverUserId,
                                        Comment = c.Comment,
                                        CreditNoteRequestId = c.CreditNoteRequestId,
                                        IsApproved = c.IsApproved,
                                        Status = c.Status,
                                        CreditNoteRequestApprovalDetailId = c.CreditNoteRequestApprovalDetailId,
                                        ActedUponOn = Convert.ToDateTime(c.CreatedOn)
                                    })
                                    .ToListAsync();
                //Gets the Credit note item that matches the id supplied
                var creditNoteRequest = await GetCreditNoteRequestById(creditRequestId);
                //Gets the assessment tied to the credit note
                var creditNoteAssessment = await GetAssessmentInfoByAssessmentRef(creditNoteRequest.AssessmentReferenceNumber);
                //Gets the reconcilliation infor tied to the credit note
                var creditNoteReconcilliationInfo = await SearchByPaymentRef(creditNoteRequest.PaymentReferenceNumber);
                //create a new instance of the CreditNoteDetail class
                var response = new CreditNoteDetail
                {
                    CreditNoteRequest = creditNoteRequest,
                    CreditNoteRequestApprovalDetails = approvalList,
                    SearchFromAssessmentDTO = creditNoteAssessment,
                    SearchFromReconciliationDTO = creditNoteReconcilliationInfo.Data
                };
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured in CreditNoteRepository, GetCreditNoteDetail");
                return null;
            }
        }

        /// <summary>
        /// Get a list of credit note request, That the current user has not attended to.
        /// </summary>
        /// <param name="userId">Current logged in userId</param>
        /// <returns>Filtered CreditNoteRequestDTO</returns>
        public async Task<List<CreditNoteRequestDTO>> GetCreditNoteRequestsNotAttendedToByUser(string userId)
        {
            var requestList = await _context.CreditNoteRequest
                                .Include(c => c.CreditNoteRequestApprovalDetails)
                                .Where(c => !c.CreditNoteRequestApprovalDetails
                                .Any(c => c.ApproverUserId == userId))
                                .OrderByDescending(c => c.DateRequested)
                                .Select(c => new CreditNoteRequestDTO
                                {
                                    ActualAmount = c.ActualAmount,
                                    AmountUsed = c.AmountUsed,
                                    ApprovalCount = c.ApprovalCount,
                                    ApprovalSettingId = c.ApprovalSettingId,
                                    DateRequested = c.DateRequested,
                                    Balance = c.Balance,
                                    CreditNoteRequestId = c.CreditNoteRequestId,
                                    DateApproved = c.DateApproved,
                                    IsApproved = c.IsApproved,
                                    MerchantCode = c.MerchantCode,
                                    NoOfRequiredApproval = c.NoOfRequiredApproval,
                                    PaymentReferenceNumber = c.PaymentReferenceNumber,
                                    RequestedBy = c.RequestedByName,
                                    AssessmentReferenceNumber = c.AssessmentReferenceNumber
                                }).ToListAsync();

            return requestList;
        }

        /// <summary>
        /// Get item from reconciliation repository
        /// </summary>
        /// <param name="paymentRef">Payment Reference</param>
        /// <returns>First or Default from Reconciliation Repository that matches the paymentRef supplied</returns>
        public async Task<RepositoryResponseDTO<SearchFromReconciliationDTO>> SearchByPaymentRef(string paymentRef)
        {
            try
            {
                var response = new RepositoryResponseDTO<SearchFromReconciliationDTO>();
                var reconciliationInfo = await _reconcileContext.Collection.Where(c => c.PaymentRefNumber.ToLower() == paymentRef.ToLower().Trim()).FirstOrDefaultAsync();
                if (reconciliationInfo is null)
                {
                    //newly added
                    //check collection revenue table
                    #region newly added by Anthony


                    //var collectionRevenueInfo = await _icmaCollectionContext.CollectionReports.FirstOrDefaultAsync(c => c.PaymentRefNumber == paymentRef && c.IsReversed == false && c.AssessmentNo == null);
                    var collectionRevenueInfo = await _icmaCollectionContext.CollectionReports.FirstOrDefaultAsync(c => c.PaymentRefNumber == paymentRef && c.IsReversed == false);
                    if (collectionRevenueInfo is null)
                    {
                        return GetResponse(404, "No record found for payment ref number specified", false);

                    }
                    else
                    {
                        if(collectionRevenueInfo.AssessmentNo != null)
                        {
                            return GetResponse(404, "This payment was made with assessment ref no.", false);
                        }
                            var creditNoteRequestsForThePaymentRef = await _context.CreditNoteRequest.Where(c => c.PaymentReferenceNumber == paymentRef).ToListAsync();
                            if (creditNoteRequestsForThePaymentRef.Any(c => c.IsApproved == null))
                            {
                                //failed because one or more items is awaiting approval
                                return GetResponse(400, "One or more occurence of the payment ref no is awaiting approval, please contact the approving officer", false);
                            }
                            else
                            {
                                if (collectionRevenueInfo.IsUsed != null)
                                {
                                    if (collectionRevenueInfo.Amount > creditNoteRequestsForThePaymentRef.Sum(c => c.AmountUsed))
                                    {
                                        return GetResponse(200, "Record found", true, new SearchFromReconciliationDTO
                                        {
                                            AgencyCode = collectionRevenueInfo.AgencyCode,
                                            RevenueName = collectionRevenueInfo.RevenueName,
                                            BankName = collectionRevenueInfo.BankName,
                                            Branchname = collectionRevenueInfo.BranchName,
                                            RevenueCode = collectionRevenueInfo.RevenueCode,
                                            Payername = collectionRevenueInfo.PayerName,
                                            PaymentDate = collectionRevenueInfo.PaymentDate,
                                            PaymentRefNumber = collectionRevenueInfo.PaymentRefNumber,
                                            Amount = collectionRevenueInfo.Amount,
                                            Balance = collectionRevenueInfo.Amount - creditNoteRequestsForThePaymentRef.Sum(c => c.AmountUsed),
                                            AgencyName = collectionRevenueInfo.AgencyName,
                                            BankCode = collectionRevenueInfo.BankCode,
                                            Branchcode = collectionRevenueInfo.BranchCode,
                                            Channel = collectionRevenueInfo.ChannelCode,
                                            UsedByPlatform = collectionRevenueInfo.IsUsed.ToString(),
                                        });
                                    }
                                    else
                                    {
                                        return GetResponse(400, "This record has been fully utilized", false);
                                    }
                                }
                                else
                                {
                                    return GetResponse(200, "Record found", true, new SearchFromReconciliationDTO
                                    {
                                        AgencyCode = collectionRevenueInfo.AgencyCode,
                                        RevenueName = collectionRevenueInfo.RevenueName,
                                        BankName = collectionRevenueInfo.BankName,
                                        Branchname = collectionRevenueInfo.BranchName,
                                        RevenueCode = collectionRevenueInfo.RevenueCode,
                                        Payername = collectionRevenueInfo.PayerName,
                                        PaymentDate = collectionRevenueInfo.PaymentDate,
                                        PaymentRefNumber = collectionRevenueInfo.PaymentRefNumber,
                                        Amount = collectionRevenueInfo.Amount,
                                        AgencyName = collectionRevenueInfo.AgencyName,
                                        BankCode = collectionRevenueInfo.BankCode,
                                        Branchcode = collectionRevenueInfo.BranchName,
                                        Channel = collectionRevenueInfo.ChannelCode,
                                        UsedByPlatform = collectionRevenueInfo.IsUsed.ToString(),
                                        Balance = collectionRevenueInfo.Amount
                                    });

                                }
                            }
                    }
                    #endregion
                }
                else if (reconciliationInfo.UsedByPlatform != null)
                {
                    var creditNoteRequestsForThePaymentRef = await _context.CreditNoteRequest.Where(c => c.PaymentReferenceNumber == paymentRef).ToListAsync();
                    if (creditNoteRequestsForThePaymentRef.Any(c => c.IsApproved == false))
                    {
                        //failed because one or more items is awaiting approval
                        return GetResponse(400, "One or more occurence of the payment ref no is awaiting approval, please contact the approving officer", false);
                    }
                    else
                    {
                        if (reconciliationInfo.Amount > creditNoteRequestsForThePaymentRef.Sum(c => c.AmountUsed))
                        {
                            return GetResponse(200, "Record found", true, new SearchFromReconciliationDTO
                            {
                                AgencyCode = reconciliationInfo.AgencyCode,
                                RevenueName = reconciliationInfo.RevenueName,
                                BankName = reconciliationInfo.BankName,
                                Branchname = reconciliationInfo.Branchname,
                                RevenueCode = reconciliationInfo.RevenueCode,
                                Payername = reconciliationInfo.Payername,
                                PaymentDate = reconciliationInfo.PaymentDate,
                                PaymentRefNumber = reconciliationInfo.PaymentRefNumber,
                                Amount = reconciliationInfo.Amount,
                                Balance = reconciliationInfo.Amount - creditNoteRequestsForThePaymentRef.Sum(c => c.AmountUsed),
                                AgencyName = reconciliationInfo.AgencyName,
                                BankCode = reconciliationInfo.BankCode,
                                Branchcode = reconciliationInfo.Branchcode,
                                Channel = reconciliationInfo.Channel,
                                UsedByPlatform = reconciliationInfo.UsedByPlatform,
                            });
                        }
                        else
                        {
                            return GetResponse(400, "This record has been fully utilized", false);
                        }
                    }
                }
                else
                {
                    return GetResponse(200, "Record found", true, new SearchFromReconciliationDTO
                    {
                        AgencyCode = reconciliationInfo.AgencyCode,
                        RevenueName = reconciliationInfo.RevenueName,
                        BankName = reconciliationInfo.BankName,
                        Branchname = reconciliationInfo.Branchname,
                        RevenueCode = reconciliationInfo.RevenueCode,
                        Payername = reconciliationInfo.Payername,
                        PaymentDate = reconciliationInfo.PaymentDate,
                        PaymentRefNumber = reconciliationInfo.PaymentRefNumber,
                        Amount = reconciliationInfo.Amount,
                        AgencyName = reconciliationInfo.AgencyName,
                        BankCode = reconciliationInfo.BankCode,
                        Branchcode = reconciliationInfo.Branchcode,
                        Channel = reconciliationInfo.Channel,
                        UsedByPlatform = reconciliationInfo.UsedByPlatform,
                        Balance = reconciliationInfo.Amount
                    });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured in CreditNoteRepository, GetReconciliationInfoByPaymentRef");
                return null;
            }


        }

        /// <summary>
        /// helper response
        /// </summary>
        /// <param name="responseCode"></param>
        /// <param name="message"></param>
        /// <param name="succeeded"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private RepositoryResponseDTO<SearchFromReconciliationDTO> GetResponse(int responseCode = 0, string message = "", bool succeeded = false, SearchFromReconciliationDTO model = null)
        {
            return new RepositoryResponseDTO<SearchFromReconciliationDTO>
            {
                ResponseCode = responseCode,
                Message = message,
                Succeeded = succeeded,
                Data = model
            };
        }
        private RepositoryResponseDTO<bool> GetApprovalResponse(int responseCode = 0, string message = "", bool succeeded = false, bool model = false)
        {
            return new RepositoryResponseDTO<bool>
            {
                ResponseCode = responseCode,
                Message = message,
                Succeeded = succeeded,
                Data = model
            };
        }




        public async Task<CreditNoteRequest> GetCreditNoteRequestById(int id)
        {
            try
            {
                var request = await _context.CreditNoteRequest
                           .Where(c => c.CreditNoteRequestId == id).FirstOrDefaultAsync();
                return request;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured on CreditNoteRequestRepository, GetCreditNoteRequestById");
                return null;
            }
        }
    }
}
