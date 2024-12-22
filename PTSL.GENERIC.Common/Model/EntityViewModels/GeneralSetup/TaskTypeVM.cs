using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup
{
    public class TaskTypeVM : BaseModel
    {
        public string TaskTypeName { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; }
        public TaskAuthority TaskAuthority { get; set; }
    }
}