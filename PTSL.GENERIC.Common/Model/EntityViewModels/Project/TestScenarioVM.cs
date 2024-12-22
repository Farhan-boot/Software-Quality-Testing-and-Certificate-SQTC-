using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class TestScenarioVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public long TaskOfProjectId { get; set; }
        public string TestScenarioNo { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string SubModule { get; set; } = string.Empty;
        public string SubModule1 { get; set; } = string.Empty;
        public string SubModule2 { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string ScenarioDescription { get; set; } = string.Empty;
        public int TC { get; set; }
        public string? POC { get; set; }
        public long UserId { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public DateTime? PlannedExecutionDate { get; set; }
        public DateTime? ActualExecutionDate { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public TaskOfProjectVM? TaskOfProject { get; set; }
    }
}
