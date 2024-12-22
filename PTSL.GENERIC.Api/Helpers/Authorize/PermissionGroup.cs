using System.Collections.Generic;

namespace PTSL.GENERIC.Api.Helpers.Authorize;

public record PermissionGroup(string GroupName, IEnumerable<IAPIPermission> Permissions);
