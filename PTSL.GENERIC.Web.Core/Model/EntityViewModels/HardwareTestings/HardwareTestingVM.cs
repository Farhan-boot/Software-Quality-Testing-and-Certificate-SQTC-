using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.HardwareTestings
{
    public class HardwareTestingVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskVM? TaskOfProject { get; set; }
        public long TestScopeId { get; set; }
        public TestScopeVM? TestScope { get; set; }
        public string SubItem { get; set; } = string.Empty;
        public string RequiredSpecification { get; set; } = string.Empty;
        public string SpecificationAsPerContract { get; set; } = string.Empty;
        public TestResult TestResult { get; set; }
        public string? Remarks { get; set; }
    }
}
