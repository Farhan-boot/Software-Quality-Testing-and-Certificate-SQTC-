namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.SystemUser;

public class RootMenuVM
{
    public string UserName { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public List<MenuVM> MenuList { get; set; } = new List<MenuVM>();
}

public class MenuVM
{
    public long ModuleID { get; set; }
    public string ModuleName { get; set; } = string.Empty;
    public string ModuleIcon { get; set; } = string.Empty;
    public List<ModuleAccessList> AccessList { get; set; } = new List<ModuleAccessList>();
}

public class ModuleAccessList
{
    public long AccessID { get; set; }
    public string ControllerName { get; set; } = string.Empty;
    public string ActionName { get; set; } = string.Empty;
    public string Mask { get; set; } = string.Empty;
    public int AccessStatus { get; set; }
    public int IsVisible { get; set; }
    public string IconClass { get; set; } = string.Empty;
    public long BaseModule { get; set; }
    public int BaseModuleIndex { get; set; }
}
