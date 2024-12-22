using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Helper;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.SecurityTestings
{
    public class SecurityTestingVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        [SwaggerExclude]
        public ProjectRequestVM? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        [SwaggerExclude]
        public TaskOfProjectVM? TaskOfProject { get; set; }
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
