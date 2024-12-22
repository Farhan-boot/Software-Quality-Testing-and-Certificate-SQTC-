using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class TaskTimeTracking : BaseEntity
    {
        public double TimeSpentHour {  get; set; }
        public double RemainingTimeHour {  get; set; }
        public string TrackingDescription { get; set; } = string.Empty;
        public TaskOfProjectStatus TaskOfProjectStatus { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }
    }
}
