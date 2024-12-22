namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings
{
    public class AttendedUserMeetingVM : BaseModel
    {
        public long MeetingId { get; set; }
        public MeetingVM? Meeting { get; set; }
        public long AttendeduserId { get; set; }
        public UserVM? AttendUser { get; set; }
    }
	
}
