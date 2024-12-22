using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
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
        public TaskPriority TaskPriority { get; set; }
        public DateTime? PlannedExecutionDate { get; set; }
        public string? PlannedExecutionDateString { get; set; }
        public DateTime? ActualExecutionDate { get; set; }
        public string? ActualExecutionDateString { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public TaskVM? TaskOfProject  { get; set; }
    }
}
