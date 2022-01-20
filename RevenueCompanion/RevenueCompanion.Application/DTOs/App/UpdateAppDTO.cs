namespace RevenueCompanion.Application.DTOs.App
{
    public class UpdateAppDTO
    {
        public int AppId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MenuUrl { get; set; }
        public string MerchantCode { get; set; }
    }
}
