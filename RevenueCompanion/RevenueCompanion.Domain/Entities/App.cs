namespace RevenueCompanion.Domain.Entities
{
    public class App : BaseEntity
    {
        public int AppId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MenuUrl { get; set; }
        public string MerchantCode { get; set; }
    }
}
