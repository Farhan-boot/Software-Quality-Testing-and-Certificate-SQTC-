using PTSL.GENERIC.Web.Core.Helper.Enum;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class TestCaseVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskVM? TaskOfProject { get; set; }
        public long TestScenarioId { get; set; }
        public TestScenarioVM? TestScenario { get; set; }
        public string TestCaseNo { get; set; } = string.Empty;
        public string TestCaseName { get; set; } = string.Empty;
        public string CaseDescription { get; set; } = string.Empty;
        public long TestCategoryId { get; set; }
        public TestCategoryVM? TestCategory { get; set; }
        // Non-Mandatory Fields
        public string? ExpectedResult { get; set; }
        public string? ActualResult { get; set; }
        public string? Comments { get; set; }
        public string? TestData { get; set; }
        public DateTime? PlannedExecutionDate { get; set; }
        public DateTime? ActualExecutionDate { get; set; }
        public bool Firefox { get; set; }
        public bool Chrome { get; set; }
        public bool IE { get; set; }
        public bool Edge { get; set; }
        public TestResult TestResult { get; set; } // Consider using Enum for TestResult
        public long ExecutedByUserId { get; set; }
        public UserVM? User { get; set; }
    }
}
