using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Entity.UserEntitys;

namespace PTSL.GENERIC.Common.Entity;

public class UserRole : BaseEntity
{
    public string RoleName { get; set; } = string.Empty;
    public string AccessList { get; set; } = string.Empty;
    public bool IsDefault {  get; set; }
    public List<User>? Users { get; set; }
    public List<UserRolePermissionMap>? UserRolePermissionMaps { get; set; }

    public List<PermissionHeaderSettings>? PermissionHeaderSettings { get; set; }
    public List<PermissionRowSettings>? PermissionRowSettings { get; set; }


}
