using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Project
{
    public class ProjectRequestVM : BaseModel
    {
        public string ProjectCode { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; }
        public ProjectApprovalStatus ProjectApprovalStatus { get; set; }
        public long ClientId { get; set; }
        public ClientVM? Client { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string? RejectionComment {  get; set; }
        public string ProjectDescription { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public long UserId { get; set; }
        public List<MeetingVM>? MeetingList { get; set; }
        public List<DocumentsByTypeVM>? documentsByTypeList { get; set; }
        public List<ProjectStateLogVM>? projectStateLogList { get; set; }

    }
}
