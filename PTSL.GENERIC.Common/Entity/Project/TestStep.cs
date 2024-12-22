using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class TestStep : BaseEntity
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }
        public long TestCaseId {  get; set; }
        public TestCase? TestCase { get; set; }
        public long TestStepId { get; set; }
        public string Test_Step { get; set; } = string.Empty;
        public string TestData { get; set; } = string.Empty;
        public string? ExpectedResult { get; set; }
        public string? ActualResult { get; set; }
    }
}
