using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Project;
using System;

namespace PTSL.GENERIC.Common.Entity.Meetings
{
    public class Meeting : BaseEntity
    {
        public long ProjectRequestId { get; set; } 
        public ProjectRequest? ProjectRequest { get; set; }
        public string MeetingTitle {  get; set; } = string.Empty;
        public string MeetingEntryPass { get; set; } = string.Empty;
        public long MeetingTypeId { get; set; }
        public MeetingType? MeetingType { get; set; }
        public DateTime? MeetingStartTime { get; set; }
        public DateTime? MeetingEndTime { get; set; }
        public string? MeetingAgenda { get; set; }
        public string? Remarks { get; set; }
        public MeetingStatus? MeetingStatus { get; set; }
        public bool? IsInitiatedBySqtc { get; set; }
        public long? ClientId { get; set; }
        public List<AttendedUserMeeting>? AttendedUsers { get; set;}
        public List<MeetingFiles>? MeetingFiles { get; set; }
    }
}

