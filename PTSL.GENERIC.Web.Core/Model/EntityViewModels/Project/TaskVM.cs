using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class TaskVM : BaseModel
    {
        public string TaskId { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; }
        public long ProjectRequestId { get; set; }
        public long TaskTypeId { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public string TaskDescription { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public double TaskEstimationHour { get; set; }
        public DateTime TaskDeadline { get; set; }
        public string TaskFileName { get; set; } = string.Empty;
        public string TaskFilePath { get; set; } = string.Empty;
        public long? UserId { get; set; }
        public double? TotalTrackedTime { get; set; }
        public double? TotalDueTime { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public TaskTypeVM? TaskType { get; set; }
        public UserVM? User { get; set; }
        public bool TimeTrackShow { get; set; }
        public TaskOfProjectStatus? TaskOfProjectStatus { get; set; }

    }
}
