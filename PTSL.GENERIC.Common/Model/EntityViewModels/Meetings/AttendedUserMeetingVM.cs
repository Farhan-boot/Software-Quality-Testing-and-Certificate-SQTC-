

using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Model.BaseModels;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Meetings
{
    public class AttendedUserMeetingVM : BaseModel
    {
        public long MeetingId { get; set; }
        public MeetingVM? Meeting { get; set; }
        public long AttendeduserId { get; set; }
        public UserVM? AttendUser { get; set; }
    }
}

