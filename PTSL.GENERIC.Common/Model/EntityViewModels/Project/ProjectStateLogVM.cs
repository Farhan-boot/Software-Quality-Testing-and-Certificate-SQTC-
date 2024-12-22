using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class ProjectStateLogVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public ProjectState ProjectState { get; set; }
        public bool IsStateCompleted { get; set; }
    }
}
