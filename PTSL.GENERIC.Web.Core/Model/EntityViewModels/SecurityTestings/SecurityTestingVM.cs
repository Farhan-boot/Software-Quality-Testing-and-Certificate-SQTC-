using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.SecurityTestings
{
    public class SecurityTestingVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskVM? TaskOfProject { get; set; }
        public SeverityLevel SeverityLevel { get; set; }
        public EaseOfExploitation EaseOfExploitation { get; set; }
        public string Vulnerability { get; set; } = string.Empty;
        public double? CVSS_Score { get; set; }
        public string VulnerableSection { get; set; } = string.Empty;
        public string? Issuedetail { get; set; }
        public string Impact { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
        public List<SecurityTestingFileVM>? SecurityTestingFiles { get; set; }
    }
}
