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
    public class TaskTimeTrackingVM : BaseModel
    {
        public double TimeSpentHour { get; set; }
        public double RemainingTimeHour { get; set; }
        public string TrackingDescription { get; set; } = string.Empty;
        public TaskOfProjectStatus TaskOfProjectStatus { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskOfProjectVM? TaskOfProject { get; set; }
    }
}
