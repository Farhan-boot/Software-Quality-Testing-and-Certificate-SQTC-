using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.DAL.Repositories.Interface.ProjectPackageConfiguration;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.ProjectPackageConfiguration
{
    public class ReconciliationRepository : BaseRepository<Reconciliation>, IReconciliationRepository
    {
        public ReconciliationRepository(
            GENERICWriteOnlyCtx writeOnlyCtx,
            GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx) { }
    }
}