
using PTSL.GENERIC.Web.Core.Helper.Enum;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class ProjectStateLogVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public ProjectState ProjectState { get; set; }
        public bool IsStateCompleted { get; set; }
    }
}
