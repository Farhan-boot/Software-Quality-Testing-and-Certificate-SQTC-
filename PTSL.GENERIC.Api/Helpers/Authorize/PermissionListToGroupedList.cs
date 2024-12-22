using System.Collections.Generic;
using System.Linq;

namespace PTSL.GENERIC.Api.Helpers.Authorize;

public static class PermissionListToGroupedListClass
{
    public static List<PermissionGroup> PermissionListToGroupedList(this IEnumerable<IAPIPermission> permissions)
    {
        return permissions
            .GroupBy(permission => permission.PermissionName.Split('.')[0])
            .Select(group => new PermissionGroup(group.Key, group.ToList()))
            .ToList();
    }
}
