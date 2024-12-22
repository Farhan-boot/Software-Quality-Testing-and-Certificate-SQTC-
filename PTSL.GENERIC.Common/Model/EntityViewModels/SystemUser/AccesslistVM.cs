using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;

public class AccesslistVM : BaseModel
{
    public string ControllerName { get; set; } = string.Empty;
    public string? ActionName { get; set; } = string.Empty;
    public string Mask { get; set; } = string.Empty;
    public byte AccessStatus { get; set; }
    public byte IsVisible { get; set; }
    public string? IconClass { get; set; } = string.Empty;
    public int? BaseModuleIndex { get; set; }
    public bool IsForClient { get; set; }
    public bool IsForSqtc { get; set; }
    public bool IsBothForSqtcAndClient { get; set; }
    public long? ModuleId { get; set; }
    public ModuleVM? Module { get; set; }

    //New Field add
    public bool? IsAvailableForApproval { get; set; }
}
