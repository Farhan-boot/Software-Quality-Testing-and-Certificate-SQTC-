using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Common.Entity.HardwareTestings
{
    public class HardwareTesting : BaseEntity
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }
        public long TestScopeId {  get; set; }
        public TestScope? TestScope { get; set; }
        public string SubItem { get; set; } = string.Empty;
        public string RequiredSpecification {  get; set; } = string.Empty;
        public string SpecificationAsPerContract { get; set; } = string.Empty;
        public TestResult TestResult { get; set; }
        public string? Remarks { get; set; }
    }
}
