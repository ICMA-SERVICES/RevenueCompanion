namespace RevenueCompanion.Application.DTOs
{
    public class RepositoryResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }

    public class ApiResponse
    {
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public bool IsSuccessful { get; set; }
    }


}
