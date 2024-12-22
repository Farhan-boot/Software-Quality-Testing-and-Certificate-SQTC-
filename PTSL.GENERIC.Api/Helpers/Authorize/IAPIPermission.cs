namespace PTSL.GENERIC.Api.Helpers.Authorize;

public interface IAPIPermission
{
    string PermissionName { get; }
    string PermissionDetails { get; }
}
