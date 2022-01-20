using System;

namespace RevenueCompanion.Application.DTOs.App
{
    public class AppDTO
    {
        public int AppId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MenuUrl { get; set; }
        public string MerchantCode { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
