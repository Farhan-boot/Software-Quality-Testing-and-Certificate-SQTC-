using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Model;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.PermissionSettings;

namespace PTSL.GENERIC.Web.Core.Services.Interface.PermissionSettings
{
    public interface IPermissionHeaderSettingsService
    {
        (ExecutionState executionState, List<PermissionHeaderSettingsVM> entity, string message) List();
        (ExecutionState executionState, PermissionHeaderSettingsVM entity, string message) Create(PermissionHeaderSettingsVM model);
        (ExecutionState executionState, PermissionHeaderSettingsVM entity, string message) GetById(long? id);
        (ExecutionState executionState, PermissionHeaderSettingsVM entity, string message) Update(PermissionHeaderSettingsVM model);
        (ExecutionState executionState, PermissionHeaderSettingsVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, List<UserVM> entity, string message) GetUserNameByUserRoleId(long userRoleId);
        (ExecutionState executionState, List<PermissionHeaderSettingsVM> entity, string message) GetPermissionHeaderSettingsByModuleEnumId(long moduleEnumId);
        (ExecutionState executionState, long entity, string message) GetPermissionHeaderIdByControllerName(string controller);
    }
}