using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.Wrappers;

namespace RevenueCompanion.Application.Constants
{
    public class ApplicationConstants
    {
        public static string SuccessResponseCode = "00";
        public static string FailureResponse = "-1";
        public static int SuccessStatusCode = 200;
        public static int NotFoundStatusCode = 404;
        public static int NotAuthenticatedStatusCode = 401;
        public static int BadRequestStatusCode = 400;



        public static string Sp_App = "Sp_App";
        public static string Sp_ApprovalSettings = "Sp_ApprovalSettings";
        public static string Sp_MenuSetup = "Sp_MenuSetup";
        public static string Sp_AppUser = "Sp_AppUser";
        public static string SP_SummaryReport = "Normalisation.SP_SummaryReport";
        public static RepositoryResponse RepositoryFailed()
        {
            var response = new RepositoryResponse
            {
                Succeeded = false,
                Message = "Failed",
                StatusCode = 0
            };
            return response;
        }
        public static RepositoryResponse RepositoryExists()
        {
            var response = new RepositoryResponse
            {
                Succeeded = false,
                Message = "already exists",
                StatusCode = -1
            };
            return response;
        }

        public static RepositoryResponse RepositorySuccess()
        {
            var response = new RepositoryResponse
            {
                Succeeded = true,
                Message = "Done",
                StatusCode = 1
            };
            return response;
        }



        public static Response<string> SuccessMessage(string message)
        {
            var response = new Response<string>
            {
                Data = message,
                Message = message,
                ResponseCode = "00",
                StatusCode = 200,
                Succeeded = true,
            };
            return response;
        }
        public static Response<T> SuccessMessage<T>(T obj, string message)
        {
            var response = new Response<T>
            {
                Data = obj,
                Message = message,
                ResponseCode = "00",
                StatusCode = 200,
                Succeeded = true,
            };
            return response;
        }

        public static Response<string> FailureMessage(string message)
        {
            var response = new Response<string>
            {
                Data = message,
                Message = message,
                ResponseCode = "-1",
                StatusCode = 400,
                Succeeded = false,
            };
            return response;
        }
        public static Response<T> FailureMessage<T>(T obj, string message)
        {
            var response = new Response<T>
            {
                Data = obj,
                Message = message,
                ResponseCode = "-1",
                StatusCode = 400,
                Succeeded = false,
            };
            return response;
        }
        public static Response<string> AlreadyExistMessage(string message)
        {
            var response = new Response<string>
            {
                Data = message,
                Message = message,
                ResponseCode = "-1",
                StatusCode = 409,
                Succeeded = false,
            };
            return response;
        }
    }
}
