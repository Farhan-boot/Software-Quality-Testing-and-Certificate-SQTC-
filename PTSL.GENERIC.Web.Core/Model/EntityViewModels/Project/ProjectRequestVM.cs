using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Sqtc_Client;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project
{
    public class ProjectRequestVM : BaseModel
    {
        public string ProjectCode { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; }
        public ProjectApprovalStatus ProjectApprovalStatus { get; set; }
        //public int ProjectTypeId { get; set; }
        public long ClientId { get; set; }
        public ClientVM? Client { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public string RequestedBy{ get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public string? ApprovalMessage { get; set; }
        public bool IsApprovalShow { get; set; }
        public string? RejectionComment {  get; set; }
        public bool IsAcceptOrReject { get; set; }
        public bool editFlag { get; set; }
        public List<MeetingVM>? MeetingList { get; set; }
        public List<DocumentsByTypeVM>? documentsByTypeList { get; set; }
        public List<ProjectStateLogVM>? projectStateLogList { get; set; }
    }
}
