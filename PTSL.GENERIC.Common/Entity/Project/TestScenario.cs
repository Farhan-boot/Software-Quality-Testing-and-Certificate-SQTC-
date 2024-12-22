using PTSL.GENERIC.Common.Entity.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class TestScenario : BaseEntity
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
        public int TC {  get; set; }
        public string? POC { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public DateTime? PlannedExecutionDate { get; set; }
        public DateTime? ActualExecutionDate { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }
    }
}
