namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

public class UserRoleVM : BaseModel
{
    public string RoleName { get; set; } = string.Empty;
    public string AccessList { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public List<UserVM>? Users { get; set; }
    public List<ModuleVM> Modules { get; set; } 
    public List<long> RoleAccessList { get; set; } 
}
