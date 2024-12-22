using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.DAL.Repositories.Interface.MeetingFiless;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{

    public class MeetingFilesRepository
	: BaseRepository<MeetingFiles>, IMeetingFilesRepository
	{
        private readonly GENERICReadOnlyCtx _readOnlyCtx;
        public MeetingFilesRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
			: base(writeOnlyCtx, readOnlyCtx)
		{
            _readOnlyCtx = readOnlyCtx;

        }

    }
}
