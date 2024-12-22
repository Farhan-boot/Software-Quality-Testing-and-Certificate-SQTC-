using PTSL.GENERIC.Business.BaseBusinesses;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration
{
    public interface IPaymentCalculationHeaderBusiness : IBaseBusiness<PaymentCalculationHeader>
    {
        Task<(ExecutionState executionState,IList<PaymentCalculationHeader> entity, string message)>ListByClientId(long clientId);
    }
}