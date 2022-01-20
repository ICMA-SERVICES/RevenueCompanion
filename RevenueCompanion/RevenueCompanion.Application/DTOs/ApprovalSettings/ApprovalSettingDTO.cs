namespace RevenueCompanion.Application.DTOs.ApprovalSettings
{
    public class ApprovalSettingDTO
    {
        public int ApprovalSettingId { get; set; }
        public int MenuSetupId { get; set; }
        public int NoOfRequiredApproval { get; set; }
        public string MerchantCode { get; set; }
        public string MenuName { get; set; }
        public string CreatedOn { get; set; }
    }
}
