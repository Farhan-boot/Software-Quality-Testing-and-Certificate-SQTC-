using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Service.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Service.Services.ProjectPackageConfiguration
{
    public interface IPaymentCalculationHeaderService : IBaseService<PaymentCalculationHeaderVM, PaymentCalculationHeader>
    {
        Task<(ExecutionState executionState, IList<PaymentCalculationHeaderVM> entity, string message)> ListByClientId(long clientId);

    }
}