using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;

namespace PTSL.GENERIC.Common.Model;

public class ModuleVM : BaseModel
{
    public string ModuleName { get; set; } = string.Empty;
    public string ModuleIcon { get; set; } = string.Empty;
    public byte IsVisible { get; set; }
    public int MenueOrder { get; set; }

    [SwaggerExclude]
    public List<AccesslistVM>? AccessList { get; set; }
}
