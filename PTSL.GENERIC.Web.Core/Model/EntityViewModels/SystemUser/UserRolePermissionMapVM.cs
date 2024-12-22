namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

public class UserRolePermissionMapVM : BaseModel
{
    public long UserRoleId { get; set; }
    public UserRoleVM? UserRole { get; set; }

    public string PermissionName { get; set; } = string.Empty;
}
