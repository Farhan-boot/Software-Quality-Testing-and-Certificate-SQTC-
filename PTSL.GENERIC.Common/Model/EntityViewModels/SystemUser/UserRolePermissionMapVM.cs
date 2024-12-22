using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;

public class UserRolePermissionMapVM : BaseModel
{
    public long UserRoleId { get; set; }
    public UserRoleVM? UserRole { get; set; }

    public string PermissionName { get; set; } = string.Empty;
}
