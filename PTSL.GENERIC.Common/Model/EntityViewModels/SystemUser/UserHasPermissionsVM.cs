using PTSL.GENERIC.Common.Helper;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;

public class UserHasPermissionsVM
{
    [SwaggerExclude]
    public long UserId { get; set; }
    public List<string> Permissions { get; set; } = new List<string>();
}
