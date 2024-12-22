namespace PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;

public class UserRoleSetAccessListsVM
{
    public long UserRoleId { get; set; }
    public List<long> AccessLists { get; set; } = new List<long>();
}
