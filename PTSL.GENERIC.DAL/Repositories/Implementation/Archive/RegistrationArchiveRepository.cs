using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.DAL.Repositories.Interface.Archive;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.Archive
{
    public class RegistrationArchiveRepository : BaseRepository<RegistrationArchive>, IRegistrationArchiveRepository
    {
        public RegistrationArchiveRepository(
            GENERICWriteOnlyCtx writeOnlyCtx,
            GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx) { }
    }
}