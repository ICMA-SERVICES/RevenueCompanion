using System.ComponentModel.DataAnnotations;

namespace RevenueCompanion.Domain.Entities
{
    public class MerchantConfig : BaseEntity
    {
        public int MerchantConfigId { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BgImage { get; set; }
        public string Color { get; set; }
        public string AppName { get; set; }
        public string BaseUrl { get; set; }
        [MaxLength(250)]
        public string StateFooter { get; set; }
        [MaxLength(250)]
        public string MerchantWebSite { get; set; }
        [MaxLength(100)]
        public string MerchantPhone { get; set; }
        [MaxLength(100)]
        public string MerchantPhone1 { get; set; }
        [MaxLength(100)]
        public string MerchantEmail { get; set; }
        [MaxLength(350)]
        public string MerchantAddress { get; set; }
        [MaxLength(350)]
        public string MerchantAddress2 { get; set; }

    }
}
