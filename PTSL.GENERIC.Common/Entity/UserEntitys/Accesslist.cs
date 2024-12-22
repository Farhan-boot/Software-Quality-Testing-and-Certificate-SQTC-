using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.PermissionSettings;

namespace PTSL.GENERIC.Common.Entity;

public class Accesslist : BaseEntity
{
    public string ControllerName { get; set; } = string.Empty;
    public string? ActionName { get; set; } = string.Empty;
    public string Mask { get; set; } = string.Empty;
    public byte AccessStatus { get; set; }
    public byte IsVisible { get; set; }
    public string? IconClass { get; set; } = string.Empty;
    public int? BaseModuleIndex { get; set; }
    public bool IsForClient {  get; set; }
    public bool IsForSqtc {  get; set; }
    public bool IsBothForSqtcAndClient {  get; set; }
    public long? ModuleId { get; set; }
    public Module? Module { get; set; }

    public bool? IsAvailableForApproval { get; set; }
    public PermissionHeaderSettings? PermissionHeaderSettings { get; set; }
}
