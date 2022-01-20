namespace RevenueCompanion.Application.DTOs.MenuSetup
{
    public class CreateMenuSetupDTO
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string ParentMenuId { get; set; }
        public string MenuUrl { get; set; }
        public bool IsActive { get; set; }
        public string IconClass { get; set; }
        public string RoleName { get; set; }
        public bool RequiresApproval { get; set; }
    }
}
