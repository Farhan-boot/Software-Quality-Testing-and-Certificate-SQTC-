using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.DAL.Repositories.Interface;

namespace PTSL.GENERIC.DAL.Repositories.Implementation
{

    public class ClientLogRepository : BaseRepository<ClientLog>, IClientLogRepository
    {
        public ClientLogRepository(
            GENERICWriteOnlyCtx ecommarceWriteOnlyCtx,
            GENERICReadOnlyCtx ecommarceReadOnlyCtx)
            : base(ecommarceWriteOnlyCtx, ecommarceReadOnlyCtx) { }
    }

}
