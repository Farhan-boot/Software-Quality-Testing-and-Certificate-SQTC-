using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IPaymentInformationService
    {
        (ExecutionState executionState, List<PaymentInformationVM> entity, string message) List();
        (ExecutionState executionState, PaymentInformationVM entity, string message) Create(PaymentInformationVM model);
        (ExecutionState executionState, PaymentInformationVM entity, string message) GetById(long? id);
        (ExecutionState executionState, PaymentInformationVM entity, string message) Update(PaymentInformationVM model);
        (ExecutionState executionState, PaymentInformationVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, bool isDeleted, string message) SoftDelete(long id);
    }
}