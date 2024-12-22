using PTSL.GENERIC.Common.Entity.CommonEntity;

namespace PTSL.GENERIC.Common.Entity.Meetings
{
    public class AttendedUserMeeting : BaseEntity
    {
        public long MeetingId { get; set; }
        public Meeting? Meeting { get; set; }
        public long AttendeduserId { get; set; }
        public User? AttendUser { get; set; }
    }
}

