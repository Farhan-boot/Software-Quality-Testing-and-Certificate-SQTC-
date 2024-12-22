using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Web.Core.Services.Interface.ProjectPackageConfiguration
{
    public interface IDepositSlipFileService
    {
        (ExecutionState executionState, List<DepositSlipFileVM> entity, string message) List();
        (ExecutionState executionState, DepositSlipFileVM entity, string message) Create(DepositSlipFileVM model);
        (ExecutionState executionState, DepositSlipFileVM entity, string message) GetById(long? id);
        (ExecutionState executionState, DepositSlipFileVM entity, string message) Update(DepositSlipFileVM model);
        (ExecutionState executionState, DepositSlipFileVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
    }
}