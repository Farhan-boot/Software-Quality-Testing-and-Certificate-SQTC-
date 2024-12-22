using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.GeneralSetup;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings
{
    public class MeetingVM : BaseModel
    {
        public long ProjectRequestId { get; set; }
        public ProjectRequestVM? ProjectRequest { get; set; }
        public string MeetingTitle { get; set; } = string.Empty;
        public string MeetingEntryPass { get; set; } = string.Empty;
        public long MeetingTypeId { get; set; }
        public MeetingTypeVM? MeetingType { get; set; }
        public DateTime? MeetingStartTime { get; set; }
        public DateTime? MeetingEndTime { get; set; }
        public string? MeetingAgenda { get; set; }
        public string? Remarks { get; set; }
        public bool? IsInitiatedBySqtc { get; set; }
        public long? ClientId { get; set; } 
        public MeetingStatus? MeetingStatus { get; set; }
        public List<AttendedUserMeetingVM>? AttendedUsers { get; set; }
        public List<UserVM> SqtcUser { get; set; } = new List<UserVM>();
        public List<UserVM> ClientUser { get; set; } = new List<UserVM>();

        public List<MeetingFilesVM>? MeetingFiles { get; set; }
    }
	
}
