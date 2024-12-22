using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity.Project
{
    public class ProjectRequest: BaseEntity
    {
        public string ProjectCode { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public ProjectType ProjectType {  get; set; }
        public ProjectApprovalStatus ProjectApprovalStatus { get; set; }
        public long ClientId { get; set; }
        public Client? Client { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string ProjectDescription {  get; set; } = string.Empty;
        public string? RejectionComment {  get; set; }
        public DateTime RequestDate { get; set; }
        public List<Meeting>? MeetingList { get; set; }
        public List<DocumentsByType>? documentsByTypeList { get; set; }
        public List<ProjectStateLog>? projectStateLogList { get; set; }
    }
}
