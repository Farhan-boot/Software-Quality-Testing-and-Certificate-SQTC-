using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Common.Entity.SecurityTestings
{
    public class SecurityTesting : BaseEntity
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequest? ProjectRequest { get; set; }
        public long TaskOfProjectId { get; set; }
        public TaskOfProject? TaskOfProject { get; set; }
        public SeverityLevel SeverityLevel { get; set; }  
        public EaseOfExploitation  EaseOfExploitation { get; set; }
        public string Vulnerability { get; set; } = string.Empty;
        public double? CVSS_Score { get; set; }
        public string VulnerableSection { get; set; } = string.Empty;
        public string? Issuedetail { get; set; }
        public string Impact { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
        public List<SecurityTestingFile>?  SecurityTestingFiles { get; set; }
    }
}
