using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class TaskCascadingDDLVM
    {
        public TaskCascadingDDLVM()
        {
            ProjectRequests = new List<ProjectRequestVM>();
            TaskTypes = new List<TaskTypeVM>();
        }
        public List<ProjectRequestVM> ProjectRequests { get; set; }
        public List<TaskTypeVM> TaskTypes { get; set; }
    }
}
