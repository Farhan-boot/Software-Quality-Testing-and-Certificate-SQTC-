using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IPaymentCalculationHeaderService
    {
        Task<(ExecutionState executionState, List<PaymentCalculationHeaderVM> entity, string message)> List();
        (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) Create(PaymentCalculationHeaderVM model);
        (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) GetById(long? id);
        (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) Update(PaymentCalculationHeaderVM model);
        (ExecutionState executionState, PaymentCalculationHeaderVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id);
        Task<(ExecutionState executionState, IList<PaymentCalculationHeaderVM> entity, string message)> ListByClientId(long clientId);

    }
}