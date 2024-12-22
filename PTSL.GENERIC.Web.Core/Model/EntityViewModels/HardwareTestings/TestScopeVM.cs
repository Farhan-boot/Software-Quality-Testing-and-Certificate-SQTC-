using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.HardwareTestings
{
    public class TestScopeVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskVM? TaskOfProject { get; set; }
        public string TestItem { get; set; } = string.Empty;
        public string TenderID { get; set; } = string.Empty;
        public string SerialNo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
