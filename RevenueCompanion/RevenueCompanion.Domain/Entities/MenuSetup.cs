using System.Collections.Generic;

namespace RevenueCompanion.Domain.Entities
{
    public class MenuSetup : BaseEntity
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
        public bool IsGeneral { get; set; }
        public List<UsersRolePermission> UsersRolePermissions { get; set; } = new List<UsersRolePermission>();
    }
}
