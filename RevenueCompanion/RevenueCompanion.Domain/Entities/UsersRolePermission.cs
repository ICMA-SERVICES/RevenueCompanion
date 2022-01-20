namespace RevenueCompanion.Domain.Entities
{
    public class UsersRolePermission : BaseEntity
    {
        public int UsersRolePermissionId { get; set; }
        public int MenuSetupId { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public MenuSetup MenuSetup { get; set; }

    }
}
