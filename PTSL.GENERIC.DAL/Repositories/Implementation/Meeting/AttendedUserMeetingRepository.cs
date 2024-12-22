using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.DAL.Repositories.Interface.AttendedUserMeetings;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{

    public class AttendedUserMeetingRepository
	: BaseRepository<AttendedUserMeeting>, IAttendedUserMeetingRepository
	{
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        public AttendedUserMeetingRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
			: base(writeOnlyCtx, readOnlyCtx)
		{
            _readOnlyCtx = readOnlyCtx;

        }

    }
}
