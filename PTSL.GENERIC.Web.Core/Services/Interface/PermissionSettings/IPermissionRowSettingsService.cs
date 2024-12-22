using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.PermissionSettings;

namespace PTSL.GENERIC.Web.Core.Services.Interface.PermissionSettings
{
    public interface IPermissionRowSettingsService
    {
        (ExecutionState executionState, List<PermissionRowSettingsVM> entity, string message) List();
        (ExecutionState executionState, PermissionRowSettingsVM entity, string message) Create(PermissionRowSettingsVM model);
        (ExecutionState executionState, PermissionRowSettingsVM entity, string message) GetById(long? id);
        (ExecutionState executionState, PermissionRowSettingsVM entity, string message) Update(PermissionRowSettingsVM model);
        (ExecutionState executionState, PermissionRowSettingsVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, List<PermissionRowSettingsVM> entity, string message) GetPermissionRowsByControllerName(string controllerName);
    }
}