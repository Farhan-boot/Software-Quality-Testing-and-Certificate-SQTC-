using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class TaskOfProject : BaseEntity
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
        public TaskOfProjectStatus? TaskOfProjectStatus { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public TaskType? TaskType { get; set; }
        public User? User { get; set; }
    }
}
