using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels;

public class UserRoleVM : BaseModel
{
    public string RoleName { get; set; } = string.Empty;
    public string AccessList { get; set; } = string.Empty;
    public bool IsDefault { get; set; }

    [SwaggerExclude]
    public List<UserVM>? Users { get; set; }
}
