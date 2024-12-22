using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity;

public class Module : BaseEntity
{
    public string ModuleName { get; set; } = string.Empty;
    public string ModuleIcon { get; set; } = string.Empty;
    public byte IsVisible { get; set; }
    public int MenueOrder { get; set; }
    public List<Accesslist>? AccessList { get; set; }
}
