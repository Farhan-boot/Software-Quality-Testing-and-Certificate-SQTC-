namespace PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;

public class UserRoleSetPermissionsVM
{
    public long UserRoleId { get; set; }
    public List<string> Permissions { get; set; } = new List<string>();
}
