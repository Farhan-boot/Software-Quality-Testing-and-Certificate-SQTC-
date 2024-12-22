using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IPaymentCalculationRowService
    {
        (ExecutionState executionState, List<PaymentCalculationRowVM> entity, string message) List();
        (ExecutionState executionState, PaymentCalculationRowVM entity, string message) Create(PaymentCalculationRowVM model);
        (ExecutionState executionState, PaymentCalculationRowVM entity, string message) GetById(long? id);
        (ExecutionState executionState, PaymentCalculationRowVM entity, string message) Update(PaymentCalculationRowVM model);
        (ExecutionState executionState, PaymentCalculationRowVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}