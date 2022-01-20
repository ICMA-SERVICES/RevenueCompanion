using System;

namespace RevenueCompanion.Application.DTOs.AppUser
{
    public class AppUserDTO
    {
        public int AppUserId { get; set; }
        public string AppCode { get; set; }
        public string MerchantCode { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string CreatedOn { get; set; }
        public DateTime? LastEnabledOn { get; set; }
        public DateTime? LastDisabledOn { get; set; }
        public string LastDisabledBy { get; set; }
        public string LastEnabledBy { get; set; }
    }
}
