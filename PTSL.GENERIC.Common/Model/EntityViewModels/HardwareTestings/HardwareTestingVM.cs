using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.HardwareTestings
{
    public class HardwareTestingVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskOfProjectVM? TaskOfProject { get; set; }
        public long TestScopeId { get; set; }
        public TestScopeVM? TestScope { get; set; }
        public string SubItem { get; set; } = string.Empty;
        public string RequiredSpecification { get; set; } = string.Empty;
        public string SpecificationAsPerContract { get; set; } = string.Empty;
        public TestResult TestResult { get; set; }
        public string? Remarks { get; set; }
    }
}
