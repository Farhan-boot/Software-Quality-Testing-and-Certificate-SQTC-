using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.DAL.Repositories.Interface.Meetings;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{

    public class MeetingRepository
	: BaseRepository<Meeting>, IMeetingRepository
	{
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        public MeetingRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
			: base(writeOnlyCtx, readOnlyCtx)
		{
            _readOnlyCtx = readOnlyCtx;

        }

    }
}
