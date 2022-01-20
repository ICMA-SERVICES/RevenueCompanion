namespace RevenueCompanion.Application.DTOs.ApprovalSettings
{
    public class CreateApprovalSettingDTO
    {
        public int MenuSetupId { get; set; }
        public int NoOfRequiredApproval { get; set; }
        public string MerchantCode { get; set; }
    }
}
