using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Model.BaseModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using System;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Meetings
{
    public class MeetingVM : BaseModel
    {
        public long ProjectRequestId { get; set; } 
        public ProjectRequestVM? ProjectRequest { get; set; }
        public string MeetingTitle {  get; set; } = string.Empty;
        public string MeetingEntryPass { get; set; } = string.Empty;
        public long MeetingTypeId { get; set; }
        public MeetingTypeVM? MeetingType { get; set; }
        public DateTime? MeetingStartTime { get; set; }
        public DateTime? MeetingEndTime { get; set; }
        public string? MeetingAgenda { get; set; }
        public string? Remarks { get; set; }
        public bool? IsInitiatedBySqtc { get; set; }
        public long? ClientId { get; set; } 
        public Client? Client { get; set; }
        public MeetingStatus? MeetingStatus { get; set; }
        public List<AttendedUserMeetingVM>? AttendedUsers { get; set; }
        public List<MeetingFilesVM>? MeetingFiles { get; set; }

    }
}

