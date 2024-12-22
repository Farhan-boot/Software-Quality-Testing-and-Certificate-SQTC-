using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.DAL.Repositories.Interface.ProjectPackageConfiguration;

namespace PTSL.GENERIC.DAL.Repositories.Implementation.ProjectPackageConfiguration
{
    public class PaymentCalculationRowRepository : BaseRepository<PaymentCalculationRow>, IPaymentCalculationRowRepository
    {
        public PaymentCalculationRowRepository(
            GENERICWriteOnlyCtx writeOnlyCtx,
            GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx) { }
    }
}