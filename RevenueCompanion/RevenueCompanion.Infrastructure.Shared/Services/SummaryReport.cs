using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DapperServices;
using RevenueCompanion.Application.DTOs.SummaryReport;
using RevenueCompanion.Application.Enums;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Domain.Common;
using RevenueCompanion.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.Infrastructure.Shared.Services
{
    public class SummaryReportRepository : ISummaryReportRepository
    {
        private readonly ILogger<ApprovalSettingRepository> _logger;
        private readonly IAuthenticatedUserService _authenticatedUser;
        //  private readonly IOptions<ConnectionStrings> _connectionString;
        private string constring;
        IOptions<ConnectionStrings> myconnectionString;
        private readonly IDapper _dapper;
        private readonly ApplicationDbContext _context;

        public SummaryReportRepository(ILogger<ApprovalSettingRepository> logger,
                               IAuthenticatedUserService authenticatedUser,
                               IOptions<ConnectionStrings> connectionString,
                               IDapper dapper,
                               ApplicationDbContext context)
        {
            _logger = logger;
            _authenticatedUser = authenticatedUser;
            myconnectionString = connectionString;
            _dapper = dapper;
            _context = context;
            constring = myconnectionString.Value.DefaultConnection;
        }

        public List<SummaryReportDTO> GetSummaryReport(string year)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("status", SummaryReportStatus.SummaryReport);
                param.Add("year", year);
                var response = _dapper.GetAll<SummaryReportDTO>(ApplicationConstants.SP_SummaryReport, myconnectionString.Value.PaymentNormalisationsConnection, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return null;
            }
        }

        public List<SummaryReportDTO> GetSummaryDetails(string payerUtin)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("status", SummaryReportStatus.SummaryDetails);
                param.Add("PayerUtin", payerUtin);
                var response = _dapper.GetAll<SummaryReportDTO>(ApplicationConstants.SP_SummaryReport, myconnectionString.Value.PaymentNormalisationsConnection, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return null;
            }
        }


        public List<SummaryReportDTO> GetDefaultersHistory(string payerUtin, string year)
        {
            try
            {
                if(payerUtin == "null" || payerUtin == "")
                {
                    payerUtin = null;
                }
                var param = new DynamicParameters();
                param.Add("status", SummaryReportStatus.DefaultersHistory);
                param.Add("PayerUtin", payerUtin);
                param.Add("Year", year);
                var response = _dapper.GetAll<SummaryReportDTO>(ApplicationConstants.SP_SummaryReport, myconnectionString.Value.PaymentNormalisationsConnection, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return null;
            }
        }

        public List<SummaryReportDTO> GetPayerList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("status", SummaryReportStatus.PayerList);
                var response = _dapper.GetAll<SummaryReportDTO>(ApplicationConstants.SP_SummaryReport, myconnectionString.Value.PaymentNormalisationsConnection, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return null;
            }
        }


    }
}
