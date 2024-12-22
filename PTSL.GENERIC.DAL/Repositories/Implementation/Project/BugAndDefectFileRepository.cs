using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Project
{
    public class BugAndDefectFileRepository : BaseRepository<BugAndDefectFile>, IBugAndDefectFileRepository
    {
        public BugAndDefectFileRepository(GENERICWriteOnlyCtx writeOnlyCtx, GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx)
        {
        }
    }
}
