using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class TaskOfProjectVM : BaseModel
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
        public double? TotalTrackedTime {  get; set; } 
        public double? TotalDueTime {  get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public TaskOfProjectStatus? TaskOfProjectStatus { get; set; }
        public TaskTypeVM? TaskType { get; set; }
        public UserVM? User { get; set; }
    }
}
