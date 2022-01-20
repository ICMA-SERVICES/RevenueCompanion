namespace RevenueCompanion.Domain.Entities
{
    public class ApprovalSetting : BaseEntity
    {
        public int ApprovalSettingId { get; set; }
        public int MenuSetupId { get; set; }
        public int NoOfRequiredApproval { get; set; }
        public MenuSetup MenuSetup { get; set; }
    }
}
