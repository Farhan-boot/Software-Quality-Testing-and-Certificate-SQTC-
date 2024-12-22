namespace PTSL.GENERIC.Web.Core.Helper;

public interface IAPIPermission
{
    string PermissionName { get; }
    string PermissionDetails { get; }
}

public class APIPermission
{
    public string PermissionName { get; set; } = string.Empty;
    public string PermissionDetails { get; set; } = string.Empty;
}
