namespace RevenueCompanion.Application.DTOs.Merchant
{
    public class CreateMerchantDTO
    {
        public string MerchantCode { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BgImage { get; set; }
        public string BaseUrl { get; set; }
        public string WebUrl { get; set; }
    }
}
