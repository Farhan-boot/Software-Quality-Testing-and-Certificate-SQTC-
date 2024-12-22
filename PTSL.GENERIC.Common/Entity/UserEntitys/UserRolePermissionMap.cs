using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity.UserEntitys;

public class UserRolePermissionMap : BaseEntity
{
    public long UserRoleId { get; set; }
    public UserRole? UserRole { get; set; }

    public string PermissionName { get; set; } = string.Empty;
}
