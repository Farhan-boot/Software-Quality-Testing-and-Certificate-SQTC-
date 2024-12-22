using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IReconciliationService
    {
        (ExecutionState executionState, List<ReconciliationVM> entity, string message) List();
        (ExecutionState executionState, ReconciliationVM entity, string message) Create(ReconciliationVM model);
        (ExecutionState executionState, ReconciliationVM entity, string message) GetById(long? id);
        (ExecutionState executionState, ReconciliationVM entity, string message) Update(ReconciliationVM model);
        (ExecutionState executionState, ReconciliationVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}