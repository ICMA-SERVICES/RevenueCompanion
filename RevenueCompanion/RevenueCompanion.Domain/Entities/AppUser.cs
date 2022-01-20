using System;

namespace RevenueCompanion.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public int AppUserId { get; set; }
        public string AppCode { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public string LastDisabledBy { get; set; }
        public DateTime? LastDisabledOn { get; set; }
        public DateTime? LastEnabledOn { get; set; }
        public string LastEnabledBy { get; set; }
    }
}
