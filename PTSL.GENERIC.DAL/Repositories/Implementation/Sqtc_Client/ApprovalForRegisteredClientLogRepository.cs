using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.DAL.Repositories.Interface;

namespace PTSL.GENERIC.DAL.Repositories.Implementation
{

    public class ApprovalForRegisteredClientLogRepository : BaseRepository<ApprovalForRegisteredClientLog>, IApprovalForRegisteredClientLogRepository
    {
        public ApprovalForRegisteredClientLogRepository(
            GENERICWriteOnlyCtx ecommarceWriteOnlyCtx,
            GENERICReadOnlyCtx ecommarceReadOnlyCtx)
            : base(ecommarceWriteOnlyCtx, ecommarceReadOnlyCtx) { }
    }

}
