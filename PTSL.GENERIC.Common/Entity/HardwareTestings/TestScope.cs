using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Common.Entity.HardwareTestings
{
    public class TestScope : BaseEntity
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }
        public string TestItem { get; set; } = string.Empty;
        public string TenderID { get; set; } = string.Empty;
        public string SerialNo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
