using PTSL.GENERIC.Web.Core.Helper.Enum.Project;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class TaskTimeTrackingVM : BaseModel
    {
        public double TimeSpentHour { get; set; }
        public double RemainingTimeHour { get; set; }
        public string TrackingDescription { get; set; } = string.Empty;
        public TaskOfProjectStatus TaskOfProjectStatus { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskVM? TaskOfProject { get; set; }
    }
}
