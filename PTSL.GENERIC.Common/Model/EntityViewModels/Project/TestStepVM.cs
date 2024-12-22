using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class TestStepVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskOfProjectVM? TaskOfProject { get; set; }
        public long TestCaseId { get; set; }
        public TestCaseVM? TestCase { get; set; }
        public long TestStepId { get; set; }   
        public string Test_Step { get; set; } = string.Empty;
        public string TestData { get; set; } = string.Empty;
        public string? ExpectedResult { get; set; }
        public string? ActualResult { get; set; }
    }
}
