namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

public class ModuleVM : BaseModel
{
    public string ModuleName { get; set; } = string.Empty;
    public string ModuleIcon { get; set; } = string.Empty;
    public byte IsVisible { get; set; }
    public int MenueOrder { get; set; }
    public List<AccesslistVM>? AccessList { get; set; }
}
