using PTSL.GENERIC.Web.Core.Helper;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

public record PermissionGroup(string GroupName, IEnumerable<APIPermission> Permissions);

