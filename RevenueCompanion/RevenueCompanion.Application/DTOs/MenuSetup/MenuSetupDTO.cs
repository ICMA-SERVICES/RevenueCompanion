﻿namespace RevenueCompanion.Application.DTOs.MenuSetup
{
    public class MenuSetupDTO
    {
        public int MenuSetupId { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string ParentMenuId { get; set; }
        public string MenuUrl { get; set; }
        public bool IsActive { get; set; }
        public string RoleId { get; set; }
        public string IconClass { get; set; }
        public string RoleName { get; set; }
        public bool RequiresApproval { get; set; }
        public string AppCode { get; set; }
        public string UserId { get; set; }
    }
}
